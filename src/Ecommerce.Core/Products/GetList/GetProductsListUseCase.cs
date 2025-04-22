using Ecommerce.Core.Products.GetById;
using Ecommerce.Domain.Products;
using Ecommerce.Extensions.Types;
using Ecommerce.Infrastructure.Repositories.Products;

namespace Ecommerce.Core.Products.GetList;

public class GetProductsListUseCase(
    IProductsRepository productsRepository
) : IGetProductsListUseCase
{
    public async Task<PaginatedEnumerable<ProductDto>> HandleAsync(PaginationQuery paginationQuery,
        CancellationToken cancellationToken = default)
    {
        PaginatedEnumerable<Product> products =
            await productsRepository.GetListAsync(paginationQuery, cancellationToken: cancellationToken);

        return products.Map(p => new ProductDto(p));
    }
}