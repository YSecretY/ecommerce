using Ecommerce.Core.Abstractions.Analytics.Models.Products;
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

    public async Task<PaginatedEnumerable<ProductSalesDto>> GetMostSoldProductsAsync(
        PaginationQuery paginationQuery,
        DateRangeQuery dateRangeQuery,
        CancellationToken cancellationToken = default)
    {
        FilterDefinition<ProductDailyStatistics>? filter = Builders<ProductDailyStatistics>.Filter.And(
            Builders<ProductDailyStatistics>.Filter.Gte(x => x.Date, dateRangeQuery.From),
            Builders<ProductDailyStatistics>.Filter.Lte(x => x.Date, dateRangeQuery.To)
        );

        AggregateCountResult? totalCount = await _collection.Aggregate()
            .Match(filter)
            .Group(x => x.ProductId, g => new { ProductId = g.Key })
            .Count()
            .FirstOrDefaultAsync(cancellationToken);

        long total = totalCount?.Count ?? 0;

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

        List<ProductSalesDto> items = result.Select(x => new ProductSalesDto(x.ProductId, x.TotalSold)).ToList();

        return new PaginatedEnumerable<ProductSalesDto>(items, paginationQuery.PageSize, paginationQuery.PageNumber, total);
    }

    public async Task<PaginatedEnumerable<ProductViewsDto>> GetMostViewedProductsAsync(PaginationQuery paginationQuery,
        DateRangeQuery dateRangeQuery, CancellationToken cancellationToken = default)
    {
        FilterDefinition<ProductDailyStatistics>? filter = Builders<ProductDailyStatistics>.Filter.And(
            Builders<ProductDailyStatistics>.Filter.Gte(x => x.Date, dateRangeQuery.From),
            Builders<ProductDailyStatistics>.Filter.Lte(x => x.Date, dateRangeQuery.To)
        );

        AggregateCountResult? totalCount = await _collection.Aggregate()
            .Match(filter)
            .Group(x => x.ProductId, g => new { ProductId = g.Key })
            .Count()
            .FirstOrDefaultAsync(cancellationToken);

        long total = totalCount?.Count ?? 0;

        var result = await _collection.Aggregate()
            .Match(filter)
            .Group(x => x.ProductId, g => new
            {
                ProductId = g.Key,
                TotalViews = g.Sum(x => x.ViewsCount)
            })
            .SortByDescending(x => x.TotalViews)
            .Skip(paginationQuery.PageSize * (paginationQuery.PageNumber - 1))
            .Limit(paginationQuery.PageSize)
            .ToListAsync(cancellationToken);

        List<ProductViewsDto> items = result.Select(x => new ProductViewsDto(x.ProductId, x.TotalViews)).ToList();

        return new PaginatedEnumerable<ProductViewsDto>(items, paginationQuery.PageSize, paginationQuery.PageNumber, total);
    }

    public async Task<int> GetProductDailySalesAsync(Guid productId, DateOnly date,
        CancellationToken cancellationToken = default)
    {
        FilterDefinition<ProductDailyStatistics>? filter = Builders<ProductDailyStatistics>.Filter.And(
            Builders<ProductDailyStatistics>.Filter.Eq(x => x.ProductId, productId),
            Builders<ProductDailyStatistics>.Filter.Eq(x => x.Date, date)
        );

        var result = await _collection.Aggregate()
            .Match(filter)
            .Group(x => x.ProductId, g => new { Total = g.Sum(x => x.SoldCount) })
            .FirstOrDefaultAsync(cancellationToken);

        return result?.Total ?? 0;
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

    public async Task<int> GetProductTotalViewsAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        FilterDefinition<ProductDailyStatistics>? filter =
            Builders<ProductDailyStatistics>.Filter.Eq(x => x.ProductId, productId);

        var result = await _collection.Aggregate()
            .Match(filter)
            .Group(x => true, g => new { Total = g.Sum(x => x.ViewsCount) })
            .FirstOrDefaultAsync(cancellationToken);

        return result?.Total ?? 0;
    }

    public async Task<long> GetTotalProductsSoldAsync(CancellationToken cancellationToken = default)
    {
        var result = await _collection.Aggregate()
            .Group(x => true, g => new { Total = g.Sum(x => x.SoldCount) })
            .FirstOrDefaultAsync(cancellationToken);

        return result?.Total ?? 0;
    }
}