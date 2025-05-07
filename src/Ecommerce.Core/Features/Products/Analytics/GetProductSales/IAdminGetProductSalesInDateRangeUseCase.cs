using Ecommerce.Extensions.Types;

namespace Ecommerce.Core.Features.Products.Analytics.GetProductSales;

public interface IAdminGetProductSalesInDateRangeUseCase
{
    public Task<int> HandleAsync(Guid productId, DateRangeQuery dateRangeQuery,
        CancellationToken cancellationToken = default);
}