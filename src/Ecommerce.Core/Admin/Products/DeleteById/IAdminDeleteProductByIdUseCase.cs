namespace Ecommerce.Core.Admin.Products.DeleteById;

public interface IAdminDeleteProductByIdUseCase
{
    public Task HandleAsync(Guid id, CancellationToken cancellationToken = default);
}