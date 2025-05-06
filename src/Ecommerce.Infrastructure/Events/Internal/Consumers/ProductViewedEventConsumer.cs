using Ecommerce.Core.Abstractions.Events.Products;
using Ecommerce.Kafka;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Infrastructure.Events.Internal.Consumers;

internal class ProductViewedEventConsumer(
    KafkaSettings settings,
    ILogger<KafkaConsumerBase<ProductViewedEvent>> logger
    // IEventHandler<ProductViewedEvent> handler
) : KafkaConsumerBase<ProductViewedEvent>(settings, logger, GroupId), IHasGroupId
{
    protected override string Topic => ProductViewedEvent.QueueName;

    protected override Task<bool> IsMessageAlreadyHandledAsync(ProductViewedEvent message,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    protected override async Task HandleAsync(ProductViewedEvent message, CancellationToken cancellationToken = default)
    {
        // await handler.HandleAsync(@event, cancellationToken);
    }

    protected override Task MarkMessageHandledAsync(ProductViewedEvent message,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public static string GroupId => $"analytics.{ProductViewedEvent.QueueName}";
}