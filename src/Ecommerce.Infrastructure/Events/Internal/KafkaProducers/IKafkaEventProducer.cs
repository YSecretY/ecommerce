using Ecommerce.Core.Abstractions.Events;

namespace Ecommerce.Infrastructure.Events.Internal.KafkaProducers;

internal interface IKafkaEventProducer<in TEvent>
    where TEvent : EventBase
{
    Task ProduceAsync(TEvent @event, CancellationToken cancellationToken = default);
}