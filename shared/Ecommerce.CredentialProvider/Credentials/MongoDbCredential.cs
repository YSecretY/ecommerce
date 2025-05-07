namespace Ecommerce.CredentialProvider.Credentials;

public class MongoDbCredential
{
    public MongoDbCredential(string connectionString, string databaseName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString, nameof(connectionString));
        ArgumentException.ThrowIfNullOrWhiteSpace(databaseName, nameof(databaseName));

        ConnectionString = connectionString;
        DatabaseName = databaseName;
    }

    public string ConnectionString { get; private set; }

    public string DatabaseName { get; private set; }
}