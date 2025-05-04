using Ecommerce.Analytics.Consumers;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Analytics;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddAnalytics(this IServiceCollection services)
    {
        services.AddConsumers();

        return services;
    }

    private static IServiceCollection AddConsumers(this IServiceCollection services)
    {
        services.AddHostedService<ProductViewedEventConsumer>();

        return services;
    }
}