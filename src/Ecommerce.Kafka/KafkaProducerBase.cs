using Confluent.Kafka;

namespace Ecommerce.Kafka;

public abstract class KafkaProducerBase(KafkaSettings settings)
{
    private readonly IProducer<string?, string> _producer = new ProducerBuilder<string?, string>(new ProducerConfig
    {
        BootstrapServers = settings.BootstrapServers,
        LingerMs = 10,
        BatchSize = 100000,
        Acks = Acks.Leader,
        CompressionType = CompressionType.Lz4
    }).Build();

    public abstract string Topic { get; }

    public async Task PublishAsync<TMessage>(TMessage message, string? key = null,
        CancellationToken cancellationToken = default)
    {
        string value = KafkaSerializer.Serialize(message);

        await _producer.ProduceAsync(Topic, new Message<string?, string> { Value = value, Key = key }, cancellationToken);
    }
}