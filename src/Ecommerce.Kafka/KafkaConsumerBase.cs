using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Kafka;

public abstract class KafkaConsumerBase<TMessage>(
    KafkaSettings settings,
    ILogger<KafkaConsumerBase<TMessage>> logger,
    string groupId
) : BackgroundService
{
    private readonly IConsumer<string, string> _consumer = new ConsumerBuilder<string, string>(new ConsumerConfig
    {
        BootstrapServers = settings.BootstrapServers,
        GroupId = groupId,
        AutoOffsetReset = AutoOffsetReset.Earliest
    }).Build();

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();

        _consumer.Subscribe(Topic);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var result = _consumer.Consume(stoppingToken);
                var message = KafkaSerializer.Deserialize<TMessage>(result.Message.Value);

                if (!await IsMessageAlreadyHandledAsync(message, stoppingToken))
                    await HandleAsync(message, stoppingToken);
            }
            catch (OperationCanceledException)
            {
                // Graceful shutdown
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex, stoppingToken);
            }
        }

        _consumer.Close();
    }

    protected abstract string Topic { get; }

    protected abstract Task<bool> IsMessageAlreadyHandledAsync(TMessage message, CancellationToken cancellationToken);

    protected abstract Task HandleAsync(TMessage @event, CancellationToken cancellationToken = default);

    protected virtual Task HandleExceptionAsync(Exception exception, CancellationToken cancellationToken = default)
    {
        logger.LogError("Error consuming kafka message: {messageType}\n{error}", typeof(TMessage).Name, exception);

        return Task.CompletedTask;
    }
}