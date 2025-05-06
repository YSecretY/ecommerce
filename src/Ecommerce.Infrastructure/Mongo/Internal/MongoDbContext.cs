using MongoDB.Driver;

namespace Ecommerce.Infrastructure.Mongo.Internal;

internal class MongoDbContext
{
    public IMongoDatabase Database { get; }

    public MongoDbContext(MongoDbSettings settings)
    {
        MongoClient client = new(settings.ConnectionString);

        Database = client.GetDatabase(settings.DatabaseName);
    }
}