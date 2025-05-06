using Ecommerce.Core.Abstractions.Analytics;
using Ecommerce.Core.Abstractions.Events.Products;
using Ecommerce.Infrastructure.Events.Internal.Services;
using Ecommerce.Kafka;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Infrastructure.Events.Internal.Consumers;

internal class ProductViewedEventConsumer(
    KafkaSettings settings,
    ILogger<KafkaConsumerBase<ProductViewedEvent>> logger,
    IAnalyticsEventHandler<ProductViewedEvent> handler,
    IEventsInfoService eventsInfoService
) : KafkaConsumerBase<ProductViewedEvent>(settings, logger, GroupId), IHasGroupId
{
    protected override string Topic => ProductViewedEvent.QueueName;

    protected override Task<bool> IsMessageAlreadyHandledAsync(ProductViewedEvent message,
        CancellationToken cancellationToken) => eventsInfoService.IsEventAlreadyProcessedAsync(message.EventId);

    protected override async Task HandleAsync(ProductViewedEvent message, CancellationToken cancellationToken = default) =>
        await handler.HandleAsync(message, cancellationToken);

    protected override Task MarkMessageHandledAsync(ProductViewedEvent message,
        CancellationToken cancellationToken = default) => eventsInfoService.MarkProcessedAsync(message.EventId);

    public static string GroupId => $"analytics.{ProductViewedEvent.QueueName}";
}