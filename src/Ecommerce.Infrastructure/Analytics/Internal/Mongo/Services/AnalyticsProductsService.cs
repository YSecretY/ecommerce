using Ecommerce.Core.Abstractions.Analytics.Services;
using Ecommerce.Extensions.Types;
using Ecommerce.Infrastructure.Analytics.Internal.Models;
using Ecommerce.Infrastructure.Mongo.Internal;
using MongoDB.Driver;

namespace Ecommerce.Infrastructure.Analytics.Internal.Mongo.Services;

internal class AnalyticsProductsService(MongoDbContext dbContext) : IAnalyticsProductsService
{
    private readonly IMongoCollection<ProductDailyStatistics> _collection =
        dbContext.Database.GetCollection<ProductDailyStatistics>(ProductDailyStatistics.CollectionName);

    public async Task<int> GetProductSalesInDateRangeAsync(Guid productId, DateRangeQuery dateRangeQuery,
        CancellationToken cancellationToken = default)
    {
        FilterDefinition<ProductDailyStatistics>? filter = Builders<ProductDailyStatistics>.Filter.And(
            Builders<ProductDailyStatistics>.Filter.Eq(x => x.ProductId, productId),
            Builders<ProductDailyStatistics>.Filter.Gte(x => x.Date, dateRangeQuery.From),
            Builders<ProductDailyStatistics>.Filter.Lte(x => x.Date, dateRangeQuery.To)
        );

        var result = await _collection.Aggregate()
            .Match(filter)
            .Group(x => x.ProductId, g => new { Total = g.Sum(x => x.SoldCount) })
            .FirstOrDefaultAsync(cancellationToken);

        return result?.Total ?? 0;
    }

    public async Task<PaginatedEnumerable<Guid>> GetMostSoldProductsAsync(PaginationQuery paginationQuery,
        DateRangeQuery dateRangeQuery,
        CancellationToken cancellationToken = default)
    {
        FilterDefinition<ProductDailyStatistics>? filter = Builders<ProductDailyStatistics>.Filter.And(
            Builders<ProductDailyStatistics>.Filter.Gte(x => x.Date, dateRangeQuery.From),
            Builders<ProductDailyStatistics>.Filter.Lte(x => x.Date, dateRangeQuery.To)
        );

        var result = await _collection.Aggregate()
            .Match(filter)
            .Group(x => x.ProductId, g => new
            {
                ProductId = g.Key,
                TotalSold = g.Sum(x => x.SoldCount)
            })
            .SortByDescending(x => x.TotalSold)
            .Skip(paginationQuery.PageSize * (paginationQuery.PageNumber - 1))
            .Limit(paginationQuery.PageSize)
            .ToListAsync(cancellationToken);

        AggregateCountResult? totalCount = await _collection.Aggregate()
            .Match(filter)
            .Group(x => x.ProductId, g => new { Count = g.Count() })
            .Count()
            .FirstOrDefaultAsync(cancellationToken);

        return new PaginatedEnumerable<Guid>(result.Select(x => x.ProductId).ToList(), paginationQuery.PageSize,
            paginationQuery.PageNumber, totalCount?.Count ?? 0);
    }

    public async Task<List<Guid>> GetMostViewedProductsAsync(int count, CancellationToken cancellationToken = default)
    {
        var result = await _collection.Aggregate()
            .Group(x => x.ProductId, g => new { ProductId = g.Key, TotalViews = g.Sum(x => x.ViewsCount) })
            .SortByDescending(x => x.TotalViews)
            .Limit(count)
            .ToListAsync(cancellationToken);

        return result.Select(x => x.ProductId).ToList();
    }

    public async Task<int> GetProductDailySalesAsync(Guid productId, DateOnly date,
        CancellationToken cancellationToken = default)
    {
        FilterDefinition<ProductDailyStatistics>? filter = Builders<ProductDailyStatistics>.Filter.And(
            Builders<ProductDailyStatistics>.Filter.Eq(x => x.ProductId, productId),
            Builders<ProductDailyStatistics>.Filter.Eq(x => x.Date, date)
        );

        ProductDailyStatistics? result = await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
        return result?.SoldCount ?? 0;
    }

    public async Task<int> GetProductTotalSalesAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        FilterDefinition<ProductDailyStatistics>? filter =
            Builders<ProductDailyStatistics>.Filter.Eq(x => x.ProductId, productId);

        var result = await _collection.Aggregate()
            .Match(filter)
            .Group(x => x.ProductId, g => new { Total = g.Sum(x => x.SoldCount) })
            .FirstOrDefaultAsync(cancellationToken);

        return result?.Total ?? 0;
    }

    public async Task<int> GetTotalProductsSoldAsync(CancellationToken cancellationToken = default)
    {
        var result = await _collection.Aggregate()
            .Group(x => true, g => new { Total = g.Sum(x => x.SoldCount) })
            .FirstOrDefaultAsync(cancellationToken);

        return result?.Total ?? 0;
    }

    public async Task<int> GetTotalProductViewsAsync(CancellationToken cancellationToken = default)
    {
        var result = await _collection.Aggregate()
            .Group(x => true, g => new { Total = g.Sum(x => x.ViewsCount) })
            .FirstOrDefaultAsync(cancellationToken);

        return result?.Total ?? 0;
    }
}