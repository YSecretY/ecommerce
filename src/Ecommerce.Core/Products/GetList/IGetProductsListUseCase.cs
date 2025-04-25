using Ecommerce.Extensions.Types;

namespace Ecommerce.Core.Products.GetList;

public interface IGetProductsListUseCase
{
    public Task<PaginatedEnumerable<ProductDto>> HandleAsync(PaginationQuery paginationQuery,
        CancellationToken cancellationToken = default);
}