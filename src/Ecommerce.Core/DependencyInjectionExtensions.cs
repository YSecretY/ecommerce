using Ecommerce.Core.Auth;
using Ecommerce.Core.Auth.Shared.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ecommerce.Core;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, JwtSettings jwtSettings)
    {
        services.TryAddSingleton<IPasswordHasher, PasswordHasher>();

        services.TryAddSingleton(jwtSettings);

        services.TryAddScoped<IAuthService, AuthService>();

        services.TryAddSingleton<IJwtHelper, JwtHelper>();
        services.TryAddSingleton<IIdentityTokenGenerator, IdentityTokenGenerator>();

        return services;
    }
}