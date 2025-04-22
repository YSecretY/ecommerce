using System.Text;
using Ecommerce.Core.Admin.Products.Create;
using Ecommerce.Core.Admin.Products.Update;
using Ecommerce.Core.Auth;
using Ecommerce.Core.Auth.Shared.Internal;
using Ecommerce.Core.Products.GetById;
using Ecommerce.Core.Products.GetList;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Core;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, JwtSettings jwtSettings)
    {
        services
            .AddShared()
            .AddAdmin()
            .AddProducts()
            .AddAuth(jwtSettings);

        return services;
    }

    private static IServiceCollection AddAuth(this IServiceCollection services, JwtSettings jwtSettings)
    {
        services.TryAddSingleton<IPasswordHasher, PasswordHasher>();
        services.TryAddSingleton(jwtSettings);
        services.TryAddScoped<IAuthService, AuthService>();
        services.TryAddSingleton<IJwtHelper, JwtHelper>();
        services.TryAddSingleton<IIdentityTokenGenerator, IdentityTokenGenerator>();

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidAlgorithms = [SecurityAlgorithms.HmacSha256]
                };
            })
            .AddCookie();

        services.AddAuthorization();

        return services;
    }

    private static IServiceCollection AddAdmin(this IServiceCollection services)
    {
        services.TryAddScoped<IAdminCreateProductUseCase, AdminCreateProductUseCase>();
        services.TryAddScoped<IAdminUpdateProductUseCase, AdminUpdateProductUseCase>();

        return services;
    }

    private static IServiceCollection AddProducts(this IServiceCollection services)
    {
        services.TryAddScoped<IGetProductByIdUseCase, GetProductByIdUseCase>();
        services.TryAddScoped<IGetProductsListUseCase, GetProductsListUseCase>();

        return services;
    }


    private static IServiceCollection AddShared(this IServiceCollection services)
    {
        return services;
    }
}