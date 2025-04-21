using Ecommerce.Extensions.Exceptions;
using Ecommerce.Extensions.Time;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ecommerce.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddExtensions(this IServiceCollection services)
    {
        services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddProblemDetails();

        services.AddExceptionHandler<GlobalExceptionHandler>();

        return services;
    }
}