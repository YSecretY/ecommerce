using Ecommerce.Domain;
using Ecommerce.Domain.Products;
using Ecommerce.Infrastructure.Database;

namespace Ecommerce.Infrastructure.Repositories.Products;

internal class ProductsRepository(
    ProductsDbContext dbContext
) : IProductsRepository
{
    public async Task AddAsync(Product product, CancellationToken cancellationToken = default) =>
        await dbContext.AddAsync(product, cancellationToken);
}