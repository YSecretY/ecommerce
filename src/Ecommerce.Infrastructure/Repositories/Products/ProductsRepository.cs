using Ecommerce.Domain;

namespace Ecommerce.Infrastructure.Repositories.Products;

internal class ProductsRepository : IProductsRepository
{
    public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
    {
        
    }
}