namespace Ecommerce.Core.Abstractions.Analytics.Services;

public interface IAnalyticsProductsService
{
    public Task<int> GetProductSalesInDateRangeAsync(Guid productId, DateOnly from, DateOnly to,
        CancellationToken cancellationToken = default);

    public Task<List<Guid>> GetMostSoldProductsAsync(int count, CancellationToken cancellationToken = default);

    public Task<List<Guid>> GetMostViewedProductsAsync(int count, CancellationToken cancellationToken = default);

    public Task<int> GetProductDailySalesAsync(Guid productId, DateOnly date, CancellationToken cancellationToken = default);

    public Task<int> GetProductTotalSalesAsync(Guid productId, CancellationToken cancellationToken = default);

    public Task<int> GetTotalProductsSoldAsync(CancellationToken cancellationToken = default);

    public Task<int> GetTotalProductViewsAsync(CancellationToken cancellationToken = default);
}