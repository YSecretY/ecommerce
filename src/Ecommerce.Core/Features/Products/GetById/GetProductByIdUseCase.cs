using Ecommerce.Core.Exceptions.Products;
using Ecommerce.Persistence.Database;
using Ecommerce.Persistence.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Core.Features.Products.GetById;

public class GetProductByIdUseCase(
    ApplicationDbContext dbContext
) : IGetProductByIdUseCase
{
    public async Task<ProductDto> HandleAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        Product product = await dbContext.Products
                              .AsNoTracking()
                              .FirstOrDefaultAsync(p => p.Id == productId, cancellationToken)
                          ?? throw new ProductNotFoundException();

        return new ProductDto(product);
    }
}