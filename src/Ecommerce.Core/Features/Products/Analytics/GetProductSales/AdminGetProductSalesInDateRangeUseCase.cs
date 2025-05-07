using Ecommerce.Core.Abstractions.Analytics.Services;
using Ecommerce.Extensions.Types;

namespace Ecommerce.Core.Features.Products.Analytics.GetProductSales;

internal class AdminGetProductSalesInDateRangeUseCase(
    IAnalyticsProductsService analyticsProductsService
) : IAdminGetProductSalesInDateRangeUseCase
{
    public async Task<int> HandleAsync(Guid productId, DateRangeQuery dateRangeQuery,
        CancellationToken cancellationToken = default) =>
        await analyticsProductsService.GetProductSalesInDateRangeAsync(productId, dateRangeQuery, cancellationToken);
}