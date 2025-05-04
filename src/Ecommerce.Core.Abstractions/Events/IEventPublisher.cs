namespace Ecommerce.Core.Abstractions.Events;

public interface IEventPublisher
{
    public Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : EventBase;
}