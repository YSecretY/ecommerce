using Ecommerce.Persistence.Database;
using Ecommerce.Persistence.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Core.Admin.Products.DeleteList;

public class AdminDeleteProductsListUseCase(
    ProductsDbContext dbContext
) : IAdminDeleteProductsListUseCase
{
    private const int BatchSize = 300;

    public async Task HandleAsync(List<Guid> ids, CancellationToken cancellationToken = default)
    {
        foreach (Guid[] currentIds in ids.Chunk(BatchSize))
        {
            List<Product> products = await dbContext.Products
                .AsNoTracking()
                .Where(p => currentIds.Contains(p.Id))
                .ToListAsync(cancellationToken);

            dbContext.Products.RemoveRange(products);
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}