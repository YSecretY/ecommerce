using Ecommerce.Core.Exceptions.Products;
using Ecommerce.Persistence.Database;
using Ecommerce.Persistence.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Core.Admin.Products.DeleteById;

public class AdminDeleteProductByIdUseCase(
    ProductsDbContext dbContext
) : IAdminDeleteProductByIdUseCase
{
    public async Task HandleAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Product product = await dbContext.Products
                              .AsNoTracking()
                              .FirstOrDefaultAsync(p => p.Id == id, cancellationToken)
                          ?? throw new ProductNotFoundException();

        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}