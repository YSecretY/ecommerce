using Ecommerce.Core.Abstractions.Events.Products;
using Ecommerce.Kafka;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Analytics.Consumers;

public class ProductViewedEventConsumer(
    KafkaSettings settings,
    ILogger<KafkaConsumerBase<ProductViewedEvent>> logger
) : KafkaConsumerBase<ProductViewedEvent>(settings, logger, "analytics.products.viewed")
{
    protected override string Topic => ProductViewedEvent.QueueName;

    protected override Task<bool> IsMessageAlreadyHandledAsync(ProductViewedEvent message,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    protected override Task HandleAsync(ProductViewedEvent @event, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}