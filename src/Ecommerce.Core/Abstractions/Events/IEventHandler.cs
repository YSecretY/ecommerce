namespace Ecommerce.Core.Abstractions.Events;

public interface IEventHandler<in TEvent>
{
    public Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default);
}