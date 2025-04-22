namespace Ecommerce.Core.Products.GetById;

public interface IGetProductByIdUseCase
{
    public Task<ProductDto> HandleAsync(Guid productId, CancellationToken cancellationToken = default);
}