using Ecommerce.HttpApi.Endpoints;
using Ecommerce.Persistence.Domain.Users;

namespace Ecommerce.HttpApi.Extensions;

public static class ApplicationExtensions
{
    public static void MapAdminProductsEndpoints(this WebApplication app) =>
        app.MapGroup("/api/v1/admin/products")
            .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString()))
            .MapAdminProductsEndpoints()
            .WithTags("admin-products");

    public static void MapAuthEndpoints(this WebApplication app) =>
        app.MapGroup("/api/v1/auth")
            .AllowAnonymous()
            .MapAuthEndpoints()
            .WithTags("auth");

    public static void MapProductsEndpoints(this WebApplication app) =>
        app.MapGroup("/api/v1/products")
            .AllowAnonymous()
            .MapProductsEndpoints()
            .WithTags("products");

    public static void MapReviewsEndpoints(this WebApplication app) =>
        app.MapGroup("/api/v1/reviews")
            .RequireAuthorization()
            .MapReviewsEndpoints()
            .WithTags("reviews");

    public static void MapRepliesEndpoints(this WebApplication app) =>
        app.MapGroup("/api/v1/replies")
            .RequireAuthorization()
            .MapRepliesEndpoints()
            .WithTags("replies");

    public static void MapOrdersEndpoints(this WebApplication app) =>
        app.MapGroup("/api/v1/orders")
            .RequireAuthorization()
            .MapOrdersEndpoints()
            .WithTags("orders");

    public static void MapUsersEndpoints(this WebApplication app) =>
        app.MapGroup("/api/v1/users")
            .RequireAuthorization()
            .MapUsersEndpoints()
            .WithTags("users");
}