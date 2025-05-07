using Ecommerce.Core.Abstractions.Analytics.Services;

namespace Ecommerce.Core.Features.Products.Analytics.GetProductDailySales;

internal class AdminGetProductDailySalesUseCase(
    IAnalyticsProductsService analyticsProductsService
) : IAdminGetProductDailySalesUseCase
{
    public async Task<int> HandleAsync(Guid productId, DateOnly date, CancellationToken cancellationToken = default) =>
        await analyticsProductsService.GetProductDailySalesAsync(productId, date, cancellationToken);
}