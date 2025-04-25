namespace Ecommerce.Core.Features.Admin.Products.Update;

public interface IAdminUpdateProductUseCase
{
    public Task HandleAsync(AdminUpdateProductCommand command, CancellationToken cancellationToken = default);
}