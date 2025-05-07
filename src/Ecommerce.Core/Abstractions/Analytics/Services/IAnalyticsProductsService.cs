using Ecommerce.Core.Abstractions.Analytics.Models.Products;
using Ecommerce.Extensions.Types;

namespace Ecommerce.Core.Abstractions.Analytics.Services;

public interface IAnalyticsProductsService
{
    public Task<int> GetProductSalesInDateRangeAsync(Guid productId, DateRangeQuery dateRangeQuery,
        CancellationToken cancellationToken = default);

    public Task<PaginatedEnumerable<ProductSalesDto>> GetMostSoldProductsAsync(PaginationQuery paginationQuery,
        DateRangeQuery dateRangeQuery,
        CancellationToken cancellationToken = default);

    public Task<List<Guid>> GetMostViewedProductsAsync(int count, CancellationToken cancellationToken = default);

    public Task<int> GetProductDailySalesAsync(Guid productId, DateOnly date, CancellationToken cancellationToken = default);

    public Task<int> GetProductTotalSalesAsync(Guid productId, CancellationToken cancellationToken = default);

    public Task<int> GetTotalProductsSoldAsync(CancellationToken cancellationToken = default);

    public Task<int> GetTotalProductViewsAsync(CancellationToken cancellationToken = default);
}