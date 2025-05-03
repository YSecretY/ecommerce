namespace Ecommerce.Kafka;

public class TopicSettings
{
    public TopicSettings(string name, int partitionsCount, int replicationFactor)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(partitionsCount, nameof(partitionsCount));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(replicationFactor, nameof(replicationFactor));

        ArgumentOutOfRangeException.ThrowIfGreaterThan(partitionsCount, 200_000, nameof(partitionsCount));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(replicationFactor, 3, nameof(replicationFactor));

        Name = name;
        PartitionsCount = partitionsCount;
        ReplicationFactor = replicationFactor;
    }

    public string Name { get; private set; }

    public int PartitionsCount { get; private set; }

    public int ReplicationFactor { get; private set; }
}