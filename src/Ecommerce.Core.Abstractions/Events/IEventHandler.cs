using Ecommerce.Core.Abstractions.Events.Products;

namespace Ecommerce.Core.Abstractions.Events;

public interface IEventHandler<in TEvent>
    where TEvent : ProductViewedEvent
{
    public Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default);
}