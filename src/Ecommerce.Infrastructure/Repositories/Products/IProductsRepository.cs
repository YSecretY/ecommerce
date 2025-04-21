using Ecommerce.Domain;

namespace Ecommerce.Infrastructure.Repositories.Products;

public interface IProductsRepository
{
    public Task AddAsync(Product product, CancellationToken cancellationToken = default);
}