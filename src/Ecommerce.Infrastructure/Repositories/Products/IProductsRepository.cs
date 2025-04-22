using Ecommerce.Domain.Products;
using Ecommerce.Extensions.Types;

namespace Ecommerce.Infrastructure.Repositories.Products;

public interface IProductsRepository
{
    public Task AddAsync(Product product, CancellationToken cancellationToken = default);

    public Task<Product?> GetByIdAsync(Guid id, bool tracking = false, CancellationToken cancellationToken = default);

    public Task<PaginatedEnumerable<Product>> GetListAsync(PaginationQuery paginationQuery, bool tracking = false,
        CancellationToken cancellationToken = default);
}