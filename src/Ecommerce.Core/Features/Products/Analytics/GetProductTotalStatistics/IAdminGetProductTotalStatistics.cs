using Ecommerce.Core.Abstractions.Analytics.Models.Products;

namespace Ecommerce.Core.Features.Products.Analytics.GetProductTotalStatistics;

public interface IAdminGetProductTotalStatistics
{
    public Task<ProductStatisticsDto> HandleAsync(Guid productId, CancellationToken cancellationToken = default);
}