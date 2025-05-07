using Ecommerce.Core.Abstractions.Analytics;
using Ecommerce.Core.Abstractions.Events.Products;
using Ecommerce.Core.Abstractions.Time;
using Ecommerce.Infrastructure.Analytics.Internal.Models;
using Ecommerce.Infrastructure.Mongo.Internal;
using MongoDB.Driver;

namespace Ecommerce.Infrastructure.Analytics.Internal.Mongo.Services.Writers;

internal class UserProductViewsStatisticsWriter(
    MongoDbContext dbContext,
    IDateTimeProvider dateTimeProvider
) : IUserProductViewsStatisticsWriter
{
    private readonly IMongoCollection<UserProductViewsStatistics> _collection =
        dbContext.Database.GetCollection<UserProductViewsStatistics>(UserProductViewsStatistics.CollectionName);

    public async Task HandleAsync(ProductViewedEvent @event, CancellationToken cancellationToken = default)
    {
        FilterDefinition<UserProductViewsStatistics>? filter = Builders<UserProductViewsStatistics>.Filter.And(
            Builders<UserProductViewsStatistics>.Filter.Eq(x => x.UserId, @event.UserId),
            Builders<UserProductViewsStatistics>.Filter.Eq(x => x.ProductId, @event.ProductId),
            Builders<UserProductViewsStatistics>.Filter.Eq(x => x.Date, dateTimeProvider.UtcToday));

        UpdateDefinition<UserProductViewsStatistics>? update = Builders<UserProductViewsStatistics>.Update
            .SetOnInsert(x => x.UserId, @event.UserId)
            .SetOnInsert(x => x.ProductId, @event.ProductId)
            .SetOnInsert(x => x.Date, dateTimeProvider.UtcToday)
            .Inc(x => x.ViewsCount, 1);

        await _collection.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true },
            cancellationToken: cancellationToken);
    }
}