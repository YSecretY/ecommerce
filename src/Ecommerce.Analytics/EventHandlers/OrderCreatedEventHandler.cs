using Ecommerce.Core.Abstractions.Events;
using Ecommerce.Core.Abstractions.Events.Orders;

namespace Ecommerce.Analytics.EventHandlers;

public class OrderCreatedEventHandler : IEventHandler<OrderCreatedEvent>
{
    public Task HandleAsync(OrderCreatedEvent @event, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}