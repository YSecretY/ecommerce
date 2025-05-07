namespace Ecommerce.Core.Abstractions.Analytics.Services;

public interface IAnalyticsUserService
{
    public Task<List<Guid>> GetUserMostViewedProductsAsync(Guid userId, int count,
        CancellationToken cancellationToken = default);

    public Task<int> GetUserTotalViewsAsync(Guid userId, CancellationToken cancellationToken = default);
}