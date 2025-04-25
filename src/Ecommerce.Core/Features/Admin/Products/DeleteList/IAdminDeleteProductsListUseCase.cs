namespace Ecommerce.Core.Features.Admin.Products.DeleteList;

public interface IAdminDeleteProductsListUseCase
{
    public Task HandleAsync(List<Guid> ids, CancellationToken cancellationToken = default);
}