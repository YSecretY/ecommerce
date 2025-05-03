using System.Text.Json;
using Confluent.Kafka;

namespace Ecommerce.Kafka;

public abstract class KafkaProducerBase(KafkaSettings settings)
{
    private readonly IProducer<string?, string> _producer = new ProducerBuilder<string?, string>(new ProducerConfig
    {
        BootstrapServers = settings.BootstrapServers,
    }).Build();

    public abstract string Topic { get; }

    public virtual string SerializeMessage<TMessage>(TMessage message) =>
        JsonSerializer.Serialize(message);

    public async Task PublishAsync<TMessage>(TMessage message, string? key = null,
        CancellationToken cancellationToken = default)
    {
        string value = SerializeMessage(message);

        await _producer.ProduceAsync(Topic, new Message<string?, string> { Value = value, Key = key }, cancellationToken);
    }
}