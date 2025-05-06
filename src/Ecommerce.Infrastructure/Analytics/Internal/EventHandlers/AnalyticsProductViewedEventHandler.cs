using Ecommerce.Core.Abstractions.Analytics;
using Ecommerce.Core.Abstractions.Events.Products;

namespace Ecommerce.Infrastructure.Analytics.Internal.EventHandlers;

internal class AnalyticsProductViewedEventHandler(
    IProductStatisticsWriter productStatisticsWriter
) : IAnalyticsEventHandler<ProductViewedEvent>
{
    public async Task HandleAsync(ProductViewedEvent @event, CancellationToken cancellationToken = default) =>
        await productStatisticsWriter.HandleAsync(@event, cancellationToken);
}