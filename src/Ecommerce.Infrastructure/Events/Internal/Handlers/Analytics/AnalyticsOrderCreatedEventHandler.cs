using Ecommerce.Core.Abstractions.Analytics;
using Ecommerce.Core.Abstractions.Events.Orders;

namespace Ecommerce.Infrastructure.Events.Internal.Handlers.Analytics;

internal class AnalyticsOrderCreatedEventHandler(
    IProductStatisticsWriter productStatisticsWriter
) : IAnalyticsEventHandler<OrderCreatedEvent>
{
    public async Task HandleAsync(OrderCreatedEvent @event, CancellationToken cancellationToken = default) =>
        await productStatisticsWriter.HandleAsync(@event, cancellationToken);
}