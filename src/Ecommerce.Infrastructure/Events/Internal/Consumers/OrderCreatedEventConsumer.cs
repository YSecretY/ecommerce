using Ecommerce.Core.Abstractions.Analytics;
using Ecommerce.Core.Abstractions.Events.Orders;
using Ecommerce.Kafka;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Infrastructure.Events.Internal.Consumers;

internal class OrderCreatedEventConsumer(
    KafkaSettings settings,
    ILogger<KafkaConsumerBase<OrderCreatedEvent>> logger,
    IAnalyticsEventHandler<OrderCreatedEvent> handler)
    : KafkaConsumerBase<OrderCreatedEvent>(settings, logger, GroupId), IHasGroupId
{
    protected override string Topic => OrderCreatedEvent.QueueName;

    protected override Task<bool> IsMessageAlreadyHandledAsync(OrderCreatedEvent message,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    protected override async Task HandleAsync(OrderCreatedEvent @event, CancellationToken cancellationToken = default)
    {
        await handler.HandleAsync(@event, cancellationToken);
    }

    public static string GroupId => $"analytics.{OrderCreatedEvent.QueueName}";
}