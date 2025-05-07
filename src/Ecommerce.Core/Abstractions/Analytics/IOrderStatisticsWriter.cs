using Ecommerce.Core.Abstractions.Events.Orders;

namespace Ecommerce.Core.Abstractions.Analytics;

public interface IOrderStatisticsWriter
{
    public Task HandleAsync(OrderCreatedEvent @event, CancellationToken cancellationToken = default);
}