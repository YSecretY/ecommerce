namespace Ecommerce.Core.Features.Products.Analytics;

public interface IAdminGetProductSalesInDateRangeUseCase
{
    public Task<int> HandleAsync(Guid productId, DateOnly from, DateOnly to, CancellationToken cancellationToken = default);
}