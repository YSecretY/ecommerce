using Ecommerce.Extensions.Types;
using Ecommerce.Persistence.Database;
using Ecommerce.Persistence.Domain.Products;
using Ecommerce.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Core.Features.Products.GetList;

public class GetProductsListUseCase(
    ApplicationDbContext dbContext
) : IGetProductsListUseCase
{
    public async Task<PaginatedEnumerable<ProductDto>> HandleAsync(PaginationQuery paginationQuery,
        CancellationToken cancellationToken = default)
    {
        PaginatedEnumerable<Product> products = await dbContext.Products
            .AsNoTracking()
            .ToPaginatedEnumerableAsync(paginationQuery, cancellationToken);

        return products.Map(p => new ProductDto(p));
    }
}