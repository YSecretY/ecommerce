using Ecommerce.Extensions.Types;

namespace Ecommerce.Core.Features.Products.GetList;

public interface IGetProductsListUseCase
{
    public Task<PaginatedEnumerable<ProductDto>> HandleAsync(PaginationQuery paginationQuery,
        CancellationToken cancellationToken = default);
}