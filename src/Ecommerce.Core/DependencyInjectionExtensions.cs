using System.Text;
using Ecommerce.Core.Features.Admin.Products.Create;
using Ecommerce.Core.Features.Admin.Products.DeleteById;
using Ecommerce.Core.Features.Admin.Products.DeleteList;
using Ecommerce.Core.Features.Admin.Products.Update;
using Ecommerce.Core.Features.Auth;
using Ecommerce.Core.Features.Auth.Shared;
using Ecommerce.Core.Features.Auth.Shared.Internal;
using Ecommerce.Core.Features.Products.GetById;
using Ecommerce.Core.Features.Products.GetList;
using Ecommerce.Core.Features.Users.Reviews.Create;
using Ecommerce.Core.Features.Users.Reviews.DeleteById;
using Ecommerce.Core.Features.Users.Reviews.GetList;
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
            .AddReviews()
            .AddAuth(jwtSettings);

        return services;
    }

    private static IServiceCollection AddAuth(this IServiceCollection services, JwtSettings jwtSettings)
    {
        services.AddHttpContextAccessor();
        services.TryAddSingleton<IPasswordHasher, PasswordHasher>();
        services.TryAddSingleton(jwtSettings);
        services.TryAddScoped<IAuthService, AuthService>();
        services.TryAddSingleton<IJwtHelper, JwtHelper>();
        services.TryAddSingleton<IIdentityTokenGenerator, IdentityTokenGenerator>();
        services.TryAddSingleton<IIdentityUserAccessor, IdentityUserAccessor>();

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
        services.TryAddScoped<IAdminDeleteProductByIdUseCase, AdminDeleteProductByIdUseCase>();
        services.TryAddScoped<IAdminDeleteProductsListUseCase, AdminDeleteProductsListUseCase>();

        return services;
    }

    private static IServiceCollection AddProducts(this IServiceCollection services)
    {
        services.TryAddScoped<IGetProductByIdUseCase, GetProductByIdUseCase>();
        services.TryAddScoped<IGetProductsListUseCase, GetProductsListUseCase>();

        return services;
    }

    private static IServiceCollection AddReviews(this IServiceCollection services)
    {
        services.TryAddScoped<IUserCreateReviewUseCase, UserCreateReviewUseCase>();
        services.TryAddScoped<IUserDeleteReviewByIdUseCase, UserDeleteReviewByIdUseCase>();
        services.TryAddScoped<IUserGetReviewsListUseCase, UserGetReviewsListUseCase>();

        return services;
    }

    private static IServiceCollection AddShared(this IServiceCollection services)
    {
        return services;
    }
}