using Ecommerce.Core.Abstractions.Events.Products;

namespace Ecommerce.Core.Abstractions.Analytics;

public interface IUserProductViewsStatisticsWriter
{
    public Task HandleAsync(ProductViewedEvent @event, CancellationToken cancellationToken = default);
}