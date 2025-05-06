namespace Ecommerce.Infrastructure.Mongo;

public class MongoDbSettings
{
    public MongoDbSettings(string connectionString, string databaseName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString, nameof(connectionString));
        ArgumentException.ThrowIfNullOrWhiteSpace(databaseName, nameof(databaseName));

        ConnectionString = connectionString;
        DatabaseName = databaseName;
    }

    public string ConnectionString { get; private set; }

    public string DatabaseName { get; private set; }
}