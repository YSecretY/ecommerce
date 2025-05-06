using Ecommerce.Core.Abstractions.Events;

namespace Ecommerce.Core.Abstractions.Analytics;

public interface IAnalyticsEventHandler<in TEvent>
    where TEvent : EventBase
{
    public Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default);
}