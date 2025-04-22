using Ecommerce.Domain.Products;

namespace Ecommerce.Infrastructure.Repositories.Products;

public interface IProductsRepository
{
    public Task AddAsync(Product product, CancellationToken cancellationToken = default);

    public Task<Product?> GetByIdAsync(Guid id, bool tracking = false, CancellationToken cancellationToken = default);
}