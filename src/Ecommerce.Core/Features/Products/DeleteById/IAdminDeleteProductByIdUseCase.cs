namespace Ecommerce.Core.Features.Products.DeleteById;

public interface IAdminDeleteProductByIdUseCase
{
    public Task HandleAsync(Guid id, CancellationToken cancellationToken = default);
}