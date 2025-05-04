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
        _consumer.Subscribe(Topic);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                ConsumeResult<string, string>? result = _consumer.Consume(stoppingToken);

                TMessage message = KafkaSerializer.Deserialize<TMessage>(result.Message.Value);

                if (!await IsMessageAlreadyHandledAsync(message, stoppingToken))
                    await HandleAsync(message, stoppingToken);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(exception, stoppingToken);
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