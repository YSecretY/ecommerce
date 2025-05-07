using Ecommerce.HttpApi.Endpoints;
using Ecommerce.Persistence.Domain.Users;

namespace Ecommerce.HttpApi.Extensions;

public static class ApplicationExtensions
{
    public static void MapAdminEndpoints(this WebApplication app) =>
        app.MapGroup("/api/v1/admin")
            .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString()))
            .MapAdminEndpoints()
            .WithTags("Admin");

    public static void MapAuthEndpoints(this WebApplication app) =>
        app.MapGroup("/api/v1/auth")
            .AllowAnonymous()
            .MapAuthEndpoints()
            .WithTags("Auth");

    public static void MapProductsEndpoints(this WebApplication app) =>
        app.MapGroup("/api/v1/products")
            .AllowAnonymous()
            .MapProductsEndpoints()
            .WithTags("Products");

    public static void MapReviewsEndpoints(this WebApplication app) =>
        app.MapGroup("/api/v1/reviews")
            .RequireAuthorization()
            .MapReviewsEndpoints()
            .WithTags("Reviews");

    public static void MapRepliesEndpoints(this WebApplication app) =>
        app.MapGroup("/api/v1/replies")
            .RequireAuthorization()
            .MapRepliesEndpoints()
            .WithTags("Replies");

    public static void MapOrdersEndpoints(this WebApplication app) =>
        app.MapGroup("/api/v1/orders")
            .RequireAuthorization()
            .MapOrdersEndpoints()
            .WithTags("Orders");

    public static void MapUsersEndpoints(this WebApplication app) =>
        app.MapGroup("/api/v1/users")
            .RequireAuthorization()
            .MapUsersEndpoints()
            .WithTags("Users");
}