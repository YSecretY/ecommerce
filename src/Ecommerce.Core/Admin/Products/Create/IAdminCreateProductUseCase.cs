namespace Ecommerce.Core.Admin.Products.Create;

public interface IAdminCreateProductUseCase
{
    public Task<Guid> HandleAsync(AdminCreateProductCommand command, CancellationToken cancellationToken = default);
}