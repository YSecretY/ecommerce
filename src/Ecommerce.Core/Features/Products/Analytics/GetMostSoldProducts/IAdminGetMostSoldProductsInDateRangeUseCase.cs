using Ecommerce.Extensions.Types;

namespace Ecommerce.Core.Features.Products.Analytics.GetMostSoldProducts;

public interface IAdminGetMostSoldProductsInDateRangeUseCase
{
    public Task<PaginatedEnumerable<ProductWithSalesInfoDto>> HandleAsync(PaginationQuery paginationQuery,
        DateRangeQuery dateRangeQuery, CancellationToken cancellationToken = default);
}