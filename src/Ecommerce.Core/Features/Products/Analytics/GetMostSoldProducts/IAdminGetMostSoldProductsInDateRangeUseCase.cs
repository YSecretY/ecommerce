using Ecommerce.Core.Abstractions.Models.Products;
using Ecommerce.Extensions.Types;

namespace Ecommerce.Core.Features.Products.Analytics.GetMostSoldProducts;

public interface IAdminGetMostSoldProductsInDateRangeUseCase
{
    public Task<PaginatedEnumerable<ProductDto>> HandleAsync(PaginationQuery paginationQuery,
        DateRangeQuery dateRangeQuery,
        CancellationToken cancellationToken = default);
}