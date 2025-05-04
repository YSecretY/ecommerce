using Ecommerce.Core.Abstractions.Events;
using Ecommerce.Infrastructure.Events.Internal.KafkaProducers;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infrastructure.Events.Internal;

internal class KafkaEventPublisher(
    IServiceProvider serviceProvider
) : IEventPublisher
{
    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : EventBase
    {
        IKafkaEventProducer<TEvent> producer = serviceProvider.GetRequiredService<IKafkaEventProducer<TEvent>>();

        await producer.ProduceAsync(@event, cancellationToken);
    }
}