using Ecommerce.Core.Abstractions.Analytics.Services;

namespace Ecommerce.Core.Features.Products.Analytics.GetTotalSold;

public class AdminGetTotalProductsSoldUseCase(
    IAnalyticsProductsService analyticsProductsService
) : IAdminGetTotalProductsSoldUseCase
{
    public async Task<long> HandleAsync(CancellationToken cancellationToken = default) =>
        await analyticsProductsService.GetTotalProductsSoldAsync(cancellationToken);
}