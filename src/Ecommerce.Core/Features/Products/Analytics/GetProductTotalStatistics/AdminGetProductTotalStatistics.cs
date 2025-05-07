using Ecommerce.Core.Abstractions.Analytics.Models.Products;
using Ecommerce.Core.Abstractions.Analytics.Services;

namespace Ecommerce.Core.Features.Products.Analytics.GetProductTotalStatistics;

internal class AdminGetProductTotalStatistics(
    IAnalyticsProductsService analyticsProductsService
) : IAdminGetProductTotalStatistics
{
    public async Task<ProductStatisticsDto> HandleAsync(Guid productId, CancellationToken cancellationToken = default) =>
        new(
            totalSales: await analyticsProductsService.GetProductTotalSalesAsync(productId, cancellationToken),
            totalViews: await analyticsProductsService.GetProductTotalViewsAsync(productId, cancellationToken)
        );
}