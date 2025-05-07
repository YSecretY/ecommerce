using Ecommerce.Core.Abstractions.Events.Orders;
using Ecommerce.Core.Abstractions.Events.Products;

namespace Ecommerce.Core.Abstractions.Analytics;

public interface IProductStatisticsWriter
{
    public Task HandleAsync(OrderCreatedEvent @event, CancellationToken cancellationToken = default);

    public Task HandleAsync(ProductViewedEvent @event, CancellationToken cancellationToken = default);
}