using Ecommerce.Analytics.EventHandlers;
using Ecommerce.Core.Abstractions.Events;
using Ecommerce.Core.Abstractions.Events.Orders;
using Ecommerce.Core.Abstractions.Events.Products;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ecommerce.Analytics;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddAnalytics(this IServiceCollection services)
    {
        services.AddEventHandlers();

        return services;
    }

    private static IServiceCollection AddEventHandlers(this IServiceCollection services)
    {
        services.TryAddSingleton<IEventHandler<ProductViewedEvent>, ProductViewedEventHandler>();
        services.TryAddSingleton<IEventHandler<OrderCreatedEvent>, OrderCreatedEventHandler>();

        return services;
    }
}