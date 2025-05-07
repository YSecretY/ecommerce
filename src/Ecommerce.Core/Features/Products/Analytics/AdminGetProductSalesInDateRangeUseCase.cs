using Ecommerce.Core.Abstractions.Analytics.Services;

namespace Ecommerce.Core.Features.Products.Analytics;

internal class AdminGetProductSalesInDateRangeUseCase(
    IAnalyticsProductsService analyticsProductsService
) : IAdminGetProductSalesInDateRangeUseCase
{
    public async Task<int> HandleAsync(Guid productId, DateOnly from, DateOnly to,
        CancellationToken cancellationToken = default) =>
        await analyticsProductsService.GetProductSalesInDateRangeAsync(productId, from, to, cancellationToken);
}