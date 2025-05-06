using Ecommerce.Core.Abstractions.Analytics;
using Ecommerce.Core.Abstractions.Events.Orders;
using Ecommerce.Infrastructure.Mongo.Internal.Services;
using Ecommerce.Kafka;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Infrastructure.Events.Internal.Consumers;

internal class OrderCreatedEventConsumer(
    KafkaSettings settings,
    ILogger<KafkaConsumerBase<OrderCreatedEvent>> logger,
    IAnalyticsEventHandler<OrderCreatedEvent> handler,
    IEventsInfoService eventsInfoService
) : KafkaConsumerBase<OrderCreatedEvent>(settings, logger, GroupId), IHasGroupId
{
    protected override string Topic => OrderCreatedEvent.QueueName;

    protected override async Task<bool> IsMessageAlreadyHandledAsync(OrderCreatedEvent message,
        CancellationToken cancellationToken) => await eventsInfoService.IsEventAlreadyProcessedAsync(message.EventId);

    protected override async Task HandleAsync(OrderCreatedEvent message, CancellationToken cancellationToken = default) =>
        await handler.HandleAsync(message, cancellationToken);

    protected override Task MarkMessageHandledAsync(OrderCreatedEvent message,
        CancellationToken cancellationToken = default) => eventsInfoService.MarkHandledAsync(message.EventId);

    public static string GroupId => $"analytics.{OrderCreatedEvent.QueueName}";
}