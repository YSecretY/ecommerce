using Ecommerce.Core.Abstractions.Analytics.Services;
using Ecommerce.Infrastructure.Analytics.Internal.Models;
using Ecommerce.Infrastructure.Mongo.Internal;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Ecommerce.Infrastructure.Analytics.Internal.Mongo.Services;

internal class AnalyticsUserService(
    MongoDbContext dbContext,
    ILogger<AnalyticsUserService> logger
) : IAnalyticsUserService
{
    private readonly IMongoCollection<UserProductViewsStatistics> _collection =
        dbContext.Database.GetCollection<UserProductViewsStatistics>(UserProductViewsStatistics.CollectionName);

    public async Task<List<Guid>> GetUserMostViewedProductsAsync(Guid userId, int count,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _collection.Aggregate()
                .Match(x => x.UserId == userId)
                .Group(x => x.ProductId, g => new
                {
                    ProductId = g.Key,
                    TotalViews = g.Sum(x => x.ViewsCount)
                })
                .SortByDescending(x => x.TotalViews)
                .Limit(count)
                .ToListAsync(cancellationToken);

            return result.Select(x => x.ProductId).ToList();
        }
        catch (Exception exception)
        {
            logger.LogError("Failed to get most viewed products for user: {error}", exception);
            return [];
        }
    }

    public async Task<int> GetUserTotalViewsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _collection.Aggregate()
                .Match(x => x.UserId == userId)
                .Group(x => x.UserId, g => new
                {
                    TotalViews = g.Sum(x => x.ViewsCount)
                })
                .FirstOrDefaultAsync(cancellationToken);

            return result?.TotalViews ?? 0;
        }
        catch (Exception exception)
        {
            logger.LogError("Failed to get total views products for user: {error}", exception);
            return 0;
        }
    }
}