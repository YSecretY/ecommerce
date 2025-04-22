using Ecommerce.Domain.Products;
using Ecommerce.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories.Products;

internal class ProductsRepository(
    ProductsDbContext dbContext
) : IProductsRepository
{
    public async Task AddAsync(Product product, CancellationToken cancellationToken = default) =>
        await dbContext.AddAsync(product, cancellationToken);

    public async Task<Product?> GetByIdAsync(Guid id, bool tracking = false, CancellationToken cancellationToken = default)
    {
        IQueryable<Product> query = dbContext.Products.AsQueryable();

        if (!tracking)
            query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}