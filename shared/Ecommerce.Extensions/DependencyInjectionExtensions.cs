using Ecommerce.Extensions.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

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