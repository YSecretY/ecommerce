using Ecommerce.Core.Abstractions.Analytics;
using Ecommerce.Core.Abstractions.Events.Orders;
using Ecommerce.Core.Abstractions.Time;
using Ecommerce.Infrastructure.Analytics.Internal.Models;
using Ecommerce.Infrastructure.Mongo.Internal;
using MongoDB.Driver;

namespace Ecommerce.Infrastructure.Analytics.Internal.Mongo.Services.Writers;

internal class OrderStatisticsWriter(
    MongoDbContext dbContext,
    IDateTimeProvider dateTimeProvider
) : IOrderStatisticsWriter
{
    private readonly IMongoCollection<OrderDailyStatistics> _collection =
        dbContext.Database.GetCollection<OrderDailyStatistics>(OrderDailyStatistics.CollectionName);

    public async Task HandleAsync(OrderCreatedEvent @event, CancellationToken cancellationToken = default)
    {
        FilterDefinition<OrderDailyStatistics>? filter = Builders<OrderDailyStatistics>.Filter.And(
            Builders<OrderDailyStatistics>.Filter.Eq(x => x.Date, dateTimeProvider.UtcToday));

        UpdateDefinition<OrderDailyStatistics>? update = Builders<OrderDailyStatistics>.Update
            .SetOnInsert(x => x.Date, dateTimeProvider.UtcToday)
            .Inc(x => x.OrdersCount, 1);

        await _collection.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true },
            cancellationToken: cancellationToken);
    }
}