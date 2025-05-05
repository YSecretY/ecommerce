namespace Ecommerce.Core.Features.Products.DeleteList;

public interface IAdminDeleteProductsListUseCase
{
    public Task HandleAsync(List<Guid> ids, CancellationToken cancellationToken = default);
}