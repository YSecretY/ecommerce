namespace Ecommerce.Core.Features.Products.Analytics.GetTotalSold;

public interface IAdminGetTotalProductsSoldUseCase
{
    public Task<long> HandleAsync(CancellationToken cancellationToken = default);
}