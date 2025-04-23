using Ecommerce.Core.Admin.Products.Update;
using Ecommerce.Core.Exceptions.Products;
using Ecommerce.Domain.Products;
using Ecommerce.Infrastructure.Repositories.Products;

namespace Ecommerce.Core.Products.GetById;

public class GetProductByIdUseCase(
    IProductsRepository productsRepository
) : IGetProductByIdUseCase
{
    public async Task<ProductDto> HandleAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        Product product = await productsRepository.GetByIdAsync(productId, cancellationToken: cancellationToken)
                          ?? throw new ProductNotFoundException();

        return new ProductDto(product);
    }
}