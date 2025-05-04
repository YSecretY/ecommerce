using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ecommerce.Kafka;

public static class DependencyInjectionExtensions
{
    public static async Task AddKafka(this IServiceCollection services, KafkaSettings settings)
    {
        services.TryAddSingleton(settings);

        await new KafkaTopicsCreator(settings).RunAsync();
    }
}