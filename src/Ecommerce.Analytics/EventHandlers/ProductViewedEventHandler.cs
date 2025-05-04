using Ecommerce.Core.Abstractions.Events;
using Ecommerce.Core.Abstractions.Events.Products;

namespace Ecommerce.Analytics.EventHandlers;

public class ProductViewedEventHandler : IEventHandler<ProductViewedEvent>
{
    public Task HandleAsync(ProductViewedEvent @event, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}