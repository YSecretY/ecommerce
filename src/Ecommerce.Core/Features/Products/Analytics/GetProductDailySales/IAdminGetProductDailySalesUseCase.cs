namespace Ecommerce.Core.Features.Products.Analytics.GetProductDailySales;

public interface IAdminGetProductDailySalesUseCase
{
    public Task<int> HandleAsync(Guid productId, DateOnly date, CancellationToken cancellationToken = default);
}