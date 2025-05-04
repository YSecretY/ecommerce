namespace Ecommerce.CredentialProvider.Credentials;

public class KafkaCredential
{
    public KafkaCredential(string bootstrapServers)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(bootstrapServers, nameof(bootstrapServers));

        BootstrapServers = bootstrapServers;
    }

    public string BootstrapServers { get; private set; }
}