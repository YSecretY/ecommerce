using Ecommerce.Core.Abstractions.Analytics.Services;
using Ecommerce.Infrastructure.Analytics.Internal.Models;
using Ecommerce.Infrastructure.Mongo.Internal;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Ecommerce.Infrastructure.Analytics.Internal.Mongo.Services;

internal class AnalyticsOrdersService(
    MongoDbContext dbContext,
    ILogger<AnalyticsOrdersService> logger
) : IAnalyticsOrdersService
{
    private readonly IMongoCollection<OrderDailyStatistics> _collection =
        dbContext.Database.GetCollection<OrderDailyStatistics>(OrderDailyStatistics.CollectionName);

    public async Task<int> GetTotalOrdersCountAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _collection
                .Aggregate()
                .Group(
                    x => true,
                    g => new { Total = g.Sum(x => x.OrdersCount) }
                )
                .FirstOrDefaultAsync(cancellationToken);

            return result?.Total ?? 0;
        }
        catch (Exception exception)
        {
            logger.LogError("Failed to get total orders count: {error}", exception);
            return 0;
        }
    }

    public async Task<int> GetOrdersCountInDateRangeAsync(DateOnly from, DateOnly to,
        CancellationToken cancellationToken = default)
    {
        try
        {
            FilterDefinition<OrderDailyStatistics>? filter = Builders<OrderDailyStatistics>.Filter.And(
                Builders<OrderDailyStatistics>.Filter.Gte(x => x.Date, from),
                Builders<OrderDailyStatistics>.Filter.Lte(x => x.Date, to));

            var result = await _collection.Aggregate()
                .Match(filter)
                .Group(
                    x => true,
                    g => new { Total = g.Sum(x => x.OrdersCount) }
                )
                .FirstOrDefaultAsync(cancellationToken);

            return result?.Total ?? 0;
        }
        catch (Exception exception)
        {
            logger.LogError("Failed to count orders in date range : {error}", exception);
            return 0;
        }
    }
}