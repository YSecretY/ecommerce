using Ecommerce.Core.Features.Orders.Create;
using Ecommerce.Core.Features.Products.Analytics.GetMostSoldProducts;
using Ecommerce.Core.Features.Products.Analytics.GetMostViewedProducts;
using Ecommerce.Core.Features.Products.Analytics.GetProductDailySales;
using Ecommerce.Core.Features.Products.Analytics.GetProductSales;
using Ecommerce.Core.Features.Products.Analytics.GetProductTotalStatistics;
using Ecommerce.Core.Features.Products.Create;
using Ecommerce.Core.Features.Products.DeleteById;
using Ecommerce.Core.Features.Products.DeleteList;
using Ecommerce.Core.Features.Products.GetById;
using Ecommerce.Core.Features.Products.GetList;
using Ecommerce.Core.Features.Products.Update;
using Ecommerce.Core.Features.Replies.Create;
using Ecommerce.Core.Features.Reviews.Create;
using Ecommerce.Core.Features.Reviews.DeleteById;
using Ecommerce.Core.Features.Reviews.GetList;
using Ecommerce.Core.Features.Users.Analytics.GetMostViewedProducts;
using Ecommerce.Core.Features.Users.Auth.Login;
using Ecommerce.Core.Features.Users.Auth.Register;
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
            .AddUsers()
            .AddReviews()
            .AddReplies()
            .AddOrders();

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

        services.TryAddScoped<IAdminGetProductSalesInDateRangeUseCase, AdminGetProductSalesInDateRangeUseCase>();
        services.TryAddScoped<IAdminGetMostSoldProductsInDateRangeUseCase, AdminGetMostSoldProductsInDateRangeUseCase>();
        services.TryAddScoped<IAdminGetMostViewedProductsInDateRangeUseCase, AdminGetMostViewedProductsInDateRangeUseCase>();
        services.TryAddScoped<IAdminGetProductDailySalesUseCase, AdminGetProductDailySalesUseCase>();
        services.TryAddScoped<IAdminGetProductTotalStatistics, AdminGetProductTotalStatistics>();

        return services;
    }

    private static IServiceCollection AddUsers(this IServiceCollection services)
    {
        services.TryAddScoped<IUserRegisterUseCase, UserRegisterUseCase>();
        services.TryAddScoped<IUserLoginCommandUseCase, UserLoginCommandUseCase>();

        services.TryAddScoped<IUserGetMostViewedProductsUseCase, UserGetMostViewedProductsUseCase>();

        return services;
    }

    private static IServiceCollection AddReviews(this IServiceCollection services)
    {
        services.TryAddScoped<IUserCreateReviewUseCase, UserCreateReviewUseCase>();
        services.TryAddScoped<IUserDeleteReviewByIdUseCase, UserDeleteReviewByIdUseCase>();
        services.TryAddScoped<IUserGetReviewsListUseCase, UserGetReviewsListUseCase>();

        return services;
    }

    private static IServiceCollection AddReplies(this IServiceCollection services)
    {
        services.TryAddScoped<IUserCreateReviewReplyUseCase, UserCreateReviewReplyUseCase>();

        return services;
    }

    private static IServiceCollection AddOrders(this IServiceCollection services)
    {
        services.TryAddScoped<IUserCreateOrderUseCase, UserCreateOrderUseCase>();

        return services;
    }

    private static IServiceCollection AddShared(this IServiceCollection services)
    {
        return services;
    }
}