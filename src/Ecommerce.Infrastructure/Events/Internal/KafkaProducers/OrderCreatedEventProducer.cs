using Ecommerce.Core.Abstractions.Events.Orders;
using Ecommerce.Kafka;

namespace Ecommerce.Infrastructure.Events.Internal.KafkaProducers;

internal class OrderCreatedEventProducer(KafkaSettings settings)
    : KafkaProducerBase(settings), IKafkaEventProducer<OrderCreatedEvent>
{
    public override string Topic { get; } = OrderCreatedEvent.QueueName;

    public async Task ProduceAsync(OrderCreatedEvent @event, CancellationToken cancellationToken = default) =>
        await PublishAsync(@event, cancellationToken: cancellationToken);
}