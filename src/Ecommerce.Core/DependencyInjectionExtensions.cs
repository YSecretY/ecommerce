using Ecommerce.Core.Auth;
using Ecommerce.Core.Auth.Register.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ecommerce.Core;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.TryAddSingleton<IPasswordHasher, PasswordHasher>();

        services.TryAddScoped<IAuthService, AuthService>();

        return services;
    }
}