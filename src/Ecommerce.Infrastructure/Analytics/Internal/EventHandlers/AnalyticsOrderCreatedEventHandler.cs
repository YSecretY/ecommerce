using Ecommerce.Core.Abstractions.Analytics;
using Ecommerce.Core.Abstractions.Events.Orders;

namespace Ecommerce.Infrastructure.Analytics.Internal.EventHandlers;

internal class AnalyticsOrderCreatedEventHandler(
    IProductStatisticsWriter productStatisticsWriter,
    IOrderStatisticsWriter orderStatisticsWriter
) : IAnalyticsEventHandler<OrderCreatedEvent>
{
    public async Task HandleAsync(OrderCreatedEvent @event, CancellationToken cancellationToken = default)
    {
        await productStatisticsWriter.HandleAsync(@event, cancellationToken);
        await orderStatisticsWriter.HandleAsync(@event, cancellationToken);
    }
}