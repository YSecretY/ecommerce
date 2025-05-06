using Ecommerce.Core.Abstractions.Analytics;
using Ecommerce.Core.Abstractions.Events.Products;

namespace Ecommerce.Infrastructure.Events.Internal.Handlers.Analytics;

internal class AnalyticsProductViewedEventHandler(
    IProductStatisticsWriter productStatisticsWriter
) : IAnalyticsEventHandler<ProductViewedEvent>
{
    public async Task HandleAsync(ProductViewedEvent @event, CancellationToken cancellationToken = default) =>
        await productStatisticsWriter.HandleAsync(@event, cancellationToken);
}