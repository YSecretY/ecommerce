using Ecommerce.Infrastructure.Analytics.Internal.Models;
using Ecommerce.Infrastructure.Analytics.Internal.Mongo.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Ecommerce.Infrastructure.Mongo.Internal;

internal class MongoDbMigrationService(
    MongoDbContext dbContext,
    ILogger<MongoDbMigrationService> logger
)
{
    private static readonly List<string> EssentialCollections =
        [ProductDailyStatistics.CollectionName, ProcessedEvent.CollectionName];

    public async Task RunMigrationsAsync(CancellationToken cancellationToken = default)
    {
        IAsyncCursor<string>? collections =
            await dbContext.Database.ListCollectionNamesAsync(cancellationToken: cancellationToken);

        List<string> collectionNames = await collections.ToListAsync(cancellationToken);

        foreach (string name in EssentialCollections.Where(name => !collectionNames.Contains(name)))
        {
            logger.LogInformation("Creating collection: {CollectionName}", name);
            await dbContext.Database.CreateCollectionAsync(name, cancellationToken: cancellationToken);
        }

        await SetupProductDailyStatisticsCollection(cancellationToken);
    }

    private async Task SetupProductDailyStatisticsCollection(CancellationToken cancellationToken = default)
    {
        IMongoCollection<ProductDailyStatistics>? collection =
            dbContext.Database.GetCollection<ProductDailyStatistics>(ProductDailyStatistics.CollectionName);

        IndexKeysDefinition<ProductDailyStatistics>? indexKeys = Builders<ProductDailyStatistics>.IndexKeys
            .Ascending(x => x.ProductId)
            .Ascending(x => x.Date);

        CreateIndexModel<ProductDailyStatistics> indexModel = new(indexKeys);

        await collection.Indexes.CreateOneAsync(indexModel, cancellationToken: cancellationToken);

        logger.LogInformation("Ensured index on {CollectionName} (ProductId + Date)", ProductDailyStatistics.CollectionName);
    }
}