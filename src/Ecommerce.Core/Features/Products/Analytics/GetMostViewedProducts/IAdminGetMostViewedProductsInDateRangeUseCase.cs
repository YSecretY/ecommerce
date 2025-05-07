using Ecommerce.Extensions.Types;

namespace Ecommerce.Core.Features.Products.Analytics.GetMostViewedProducts;

public interface IAdminGetMostViewedProductsInDateRangeUseCase
{
    public Task<PaginatedEnumerable<ProductWithViewsInfoDto>> HandleAsync(PaginationQuery paginationQuery,
        DateRangeQuery dateRangeQuery, CancellationToken cancellationToken = default);
}