using Ecommerce.Core.Extensions.Products;
using Ecommerce.Persistence.Database;
using Ecommerce.Persistence.Domain.Products;
using Ecommerce.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Core.Features.Admin.Products.DeleteList;

public class AdminDeleteProductsListUseCase(
    ApplicationDbContext dbContext
) : IAdminDeleteProductsListUseCase
{
    private const int BatchSize = 300;

    public async Task HandleAsync(List<Guid> ids, CancellationToken cancellationToken = default)
    {
        foreach (Guid[] currentIds in ids.Chunk(BatchSize))
        {
            List<Product> products = await dbContext.Products
                .IncludeToSoftDelete()
                .Where(p => currentIds.Contains(p.Id))
                .ToListAsync(cancellationToken);

            dbContext.SoftDeleteRange(products);
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}