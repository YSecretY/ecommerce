namespace Ecommerce.Core.Abstractions.Analytics.Services;

public interface IAnalyticsOrdersService
{
    public Task<int> GetOrdersCountInDateRangeAsync(DateOnly from, DateOnly to,
        CancellationToken cancellationToken = default);

    public Task<int> GetTotalOrdersCountAsync(CancellationToken cancellationToken = default);
}