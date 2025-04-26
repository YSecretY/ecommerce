using Ecommerce.Core.Features.Admin.Products.Create;
using Ecommerce.Core.Features.Admin.Products.DeleteById;
using Ecommerce.Core.Features.Admin.Products.DeleteList;
using Ecommerce.Core.Features.Admin.Products.Update;
using Ecommerce.Core.Features.Products.GetById;
using Ecommerce.Core.Features.Products.GetList;
using Ecommerce.Core.Features.Users.Auth.Login;
using Ecommerce.Core.Features.Users.Auth.Register;
using Ecommerce.Core.Features.Users.Reviews.Create;
using Ecommerce.Core.Features.Users.Reviews.DeleteById;
using Ecommerce.Core.Features.Users.Reviews.GetList;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ecommerce.Core;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services
            .AddShared()
            .AddAdmin()
            .AddProducts()
            .AddUsers();

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

    private static IServiceCollection AddUsers(this IServiceCollection services)
    {
        services.TryAddScoped<IUserRegisterUseCase, UserRegisterUseCase>();
        services.TryAddScoped<IUserLoginCommandUseCase, UserLoginCommandUseCase>();

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