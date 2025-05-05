using Ecommerce.Core.Abstractions.Models.Products;

namespace Ecommerce.Core.Features.Products.GetById;

public interface IGetProductByIdUseCase
{
    public Task<ProductDto> HandleAsync(Guid productId, CancellationToken cancellationToken = default);
}