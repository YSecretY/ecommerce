using Ecommerce.Core.Exceptions.Products;
using Ecommerce.Core.Extensions.Products;
using Ecommerce.Persistence.Database;
using Ecommerce.Persistence.Domain.Products;
using Ecommerce.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Core.Features.Admin.Products.DeleteById;

public class AdminDeleteProductByIdUseCase(
    ProductsDbContext dbContext
) : IAdminDeleteProductByIdUseCase
{
    public async Task HandleAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Product product = await dbContext.Products
                              .IncludeToSoftDelete()
                              .FirstOrDefaultAsync(p => p.Id == id, cancellationToken)
                          ?? throw new ProductNotFoundException();

        dbContext.SoftDelete(product);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}