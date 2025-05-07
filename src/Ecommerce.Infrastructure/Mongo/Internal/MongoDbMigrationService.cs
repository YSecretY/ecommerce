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
        [ProductDailyStatistics.CollectionName, ProcessedEvent.CollectionName, UserProductViewsStatistics.CollectionName];

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
        await SetupUserViewsStatisticsCollection(cancellationToken);
        await SetupOrderDailyStatisticsCollection(cancellationToken);
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

    private async Task SetupUserViewsStatisticsCollection(CancellationToken cancellationToken = default)
    {
        IMongoCollection<UserProductViewsStatistics>? collection =
            dbContext.Database.GetCollection<UserProductViewsStatistics>(UserProductViewsStatistics.CollectionName);

        IndexKeysDefinition<UserProductViewsStatistics>? index1 = Builders<UserProductViewsStatistics>.IndexKeys
            .Ascending(x => x.UserId)
            .Ascending(x => x.ProductId)
            .Descending(x => x.ViewsCount);

        IndexKeysDefinition<UserProductViewsStatistics>? index2 = Builders<UserProductViewsStatistics>.IndexKeys
            .Ascending(x => x.ProductId)
            .Descending(x => x.ViewsCount);

        IndexKeysDefinition<UserProductViewsStatistics>? index3 = Builders<UserProductViewsStatistics>.IndexKeys
            .Ascending(x => x.Date)
            .Ascending(x => x.ProductId);

        await collection.Indexes.CreateManyAsync([
            new CreateIndexModel<UserProductViewsStatistics>(index1),
            new CreateIndexModel<UserProductViewsStatistics>(index2),
            new CreateIndexModel<UserProductViewsStatistics>(index3)
        ], cancellationToken: cancellationToken);

        logger.LogInformation("Ensured indexes on {CollectionName}", UserProductViewsStatistics.CollectionName);
    }

    private async Task SetupOrderDailyStatisticsCollection(CancellationToken cancellationToken = default)
    {
        IMongoCollection<OrderDailyStatistics>? collection =
            dbContext.Database.GetCollection<OrderDailyStatistics>(OrderDailyStatistics.CollectionName);

        IndexKeysDefinition<OrderDailyStatistics>? index = Builders<OrderDailyStatistics>.IndexKeys
            .Ascending(x => x.Date);

        await collection.Indexes.CreateOneAsync(new CreateIndexModel<OrderDailyStatistics>(index),
            cancellationToken: cancellationToken);

        logger.LogInformation("Ensured index on {CollectionName} (Date)", OrderDailyStatistics.CollectionName);
    }
}