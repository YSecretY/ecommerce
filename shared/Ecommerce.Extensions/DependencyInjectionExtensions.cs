using Ecommerce.Extensions.Exceptions;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddExtensions(this IServiceCollection services)
    {
        services.AddProblemDetails();

        services.AddExceptionHandler<GlobalExceptionHandler>();

        services.AddHttpContextAccessor();

        return services;
    }
}