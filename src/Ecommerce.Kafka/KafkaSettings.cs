namespace Ecommerce.Kafka;

public class KafkaSettings(List<TopicSettings> topics, string bootstrapServers)
{
    public List<TopicSettings> Topics { get; private set; } = topics;

    public string BootstrapServers { get; private set; } = bootstrapServers;
}