using Ecommerce.Core.Abstractions.Analytics;
using Ecommerce.Core.Abstractions.Events.Orders;
using Ecommerce.Core.Abstractions.Events.Products;
using Ecommerce.Core.Abstractions.Models.Orders;
using Ecommerce.Core.Abstractions.Time;
using Ecommerce.Infrastructure.Analytics.Internal.Models;
using Ecommerce.Infrastructure.Mongo.Internal;
using MongoDB.Driver;

namespace Ecommerce.Infrastructure.Analytics.Internal.Mongo.Services;

internal class ProductStatisticsWriter(
    MongoDbContext dbContext,
    IDateTimeProvider dateTimeProvider
) : IProductStatisticsWriter
{
    private readonly IMongoCollection<ProductDailyStatistics> _collection =
        dbContext.Database.GetCollection<ProductDailyStatistics>(ProductDailyStatistics.CollectionName);

    public async Task HandleAsync(OrderCreatedEvent @event, CancellationToken cancellationToken = default)
    {
        foreach (OrderItemDto item in @event.OrderDto.Items)
        {
            FilterDefinition<ProductDailyStatistics> filter = Builders<ProductDailyStatistics>.Filter.And(
                Builders<ProductDailyStatistics>.Filter.Eq(s => s.ProductId, item.ProductId),
                Builders<ProductDailyStatistics>.Filter.Eq(s => s.Date, dateTimeProvider.UtcToday));

            UpdateDefinition<ProductDailyStatistics> update = Builders<ProductDailyStatistics>.Update
                .SetOnInsert(s => s.ProductId, item.ProductId)
                .SetOnInsert(s => s.Date, dateTimeProvider.UtcToday)
                .SetOnInsert(s => s.ViewsCount, 1)
                .Inc(s => s.SoldCount, item.Quantity);

            await _collection.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true }, cancellationToken);
        }
    }

    public async Task HandleAsync(ProductViewedEvent @event, CancellationToken cancellationToken = default)
    {
        FilterDefinition<ProductDailyStatistics> filter = Builders<ProductDailyStatistics>.Filter.And(
            Builders<ProductDailyStatistics>.Filter.Eq(s => s.ProductId, @event.ProductId),
            Builders<ProductDailyStatistics>.Filter.Eq(s => s.Date, dateTimeProvider.UtcToday));

        UpdateDefinition<ProductDailyStatistics> update = Builders<ProductDailyStatistics>.Update
            .SetOnInsert(s => s.ProductId, @event.ProductId)
            .SetOnInsert(s => s.Date, dateTimeProvider.UtcToday)
            .SetOnInsert(s => s.SoldCount, 0)
            .Inc(s => s.ViewsCount, 1);

        await _collection.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true }, cancellationToken);
    }
}