namespace Ecommerce.Core.Features.Products.Update;

public interface IAdminUpdateProductUseCase
{
    public Task HandleAsync(AdminUpdateProductCommand command, CancellationToken cancellationToken = default);
}