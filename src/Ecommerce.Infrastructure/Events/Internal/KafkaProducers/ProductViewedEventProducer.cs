using Ecommerce.Core.Abstractions.Events.Products;
using Ecommerce.Kafka;

namespace Ecommerce.Infrastructure.Events.Internal.KafkaProducers;

internal class ProductViewedEventProducer(KafkaSettings settings)
    : KafkaProducerBase(settings), IKafkaEventProducer<ProductViewedEvent>
{
    public override string Topic => ProductViewedEvent.QueueName;

    public async Task ProduceAsync(ProductViewedEvent @event, CancellationToken cancellationToken = default) =>
        await PublishAsync(@event, cancellationToken: cancellationToken);
}