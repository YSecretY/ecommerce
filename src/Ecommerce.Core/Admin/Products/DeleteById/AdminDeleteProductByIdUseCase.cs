using Ecommerce.Core.Admin.Products.Update;
using Ecommerce.Domain.Products;
using Ecommerce.Infrastructure.Database.Products;
using Ecommerce.Infrastructure.Repositories.Products;

namespace Ecommerce.Core.Admin.Products.DeleteById;

public class AdminDeleteProductByIdUseCase(
    IProductsRepository productsRepository,
    IProductsUnitOfWork unitOfWork
) : IAdminDeleteProductByIdUseCase
{
    public async Task HandleAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Product product = await productsRepository.GetByIdAsync(id, cancellationToken: cancellationToken)
                          ?? throw new ProductNotFoundException();

        productsRepository.Remove(product);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}