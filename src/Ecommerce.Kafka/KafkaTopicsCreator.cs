using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Ecommerce.Extensions.Types;

namespace Ecommerce.Kafka;

internal class KafkaTopicsCreator(KafkaSettings settings)
{
    private readonly AdminClientConfig _adminConfig = new()
    {
        BootstrapServers = settings.BootstrapServers,
        AllowAutoCreateTopics = false
    };

    public async Task RunAsync()
    {
        using IAdminClient adminClient = new AdminClientBuilder(_adminConfig).Build()
                                         ?? throw new ArgumentNullException(nameof(adminClient));

        Metadata metadata = adminClient.GetMetadata(TimeSpan.FromSeconds(5))
                            ?? throw new ArgumentNullException(nameof(metadata));

        HashSet<string> existingTopics = metadata.Topics.Select(t => t.Topic).ToHashSet();

        List<TopicSpecification> newTopics = settings.Topics
            .Where(t => !existingTopics.Contains(t.Name))
            .Select(t => new TopicSpecification
            {
                Name = t.Name,
                NumPartitions = t.PartitionsCount,
                ReplicationFactor = (short)t.ReplicationFactor
            }).ToList();

        if (newTopics.IsNotEmpty())
            await adminClient.CreateTopicsAsync(newTopics);
    }
}