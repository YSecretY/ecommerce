using Ecommerce.Core.Abstractions.Events;
using Ecommerce.Core.Abstractions.Events.Products;
using Ecommerce.Kafka;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Infrastructure.Events.Internal.Consumers;

public class ProductViewedEventConsumer(
    KafkaSettings settings,
    ILogger<KafkaConsumerBase<ProductViewedEvent>> logger,
    IEventHandler<ProductViewedEvent> handler
) : KafkaConsumerBase<ProductViewedEvent>(settings, logger, GroupId), IHasGroupId
{
    protected override string Topic => ProductViewedEvent.QueueName;

    protected override Task<bool> IsMessageAlreadyHandledAsync(ProductViewedEvent message,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    protected override async Task HandleAsync(ProductViewedEvent @event, CancellationToken cancellationToken = default)
    {
        await handler.HandleAsync(@event, cancellationToken);
    }

    public static string GroupId => $"analytics.{ProductViewedEvent.QueueName}";
}