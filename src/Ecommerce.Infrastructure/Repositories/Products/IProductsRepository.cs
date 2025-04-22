using Ecommerce.Domain;
using Ecommerce.Domain.Products;

namespace Ecommerce.Infrastructure.Repositories.Products;

public interface IProductsRepository
{
    public Task AddAsync(Product product, CancellationToken cancellationToken = default);
}