using Ecommerce.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ecommerce.Persistence;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, string productsDbConnection,
        string usersDbConnection)
    {
        SoftDeleteInterceptor softDeleteInterceptor = new();

        services.TryAddSingleton(softDeleteInterceptor);

        services.AddDbContext<ProductsDbContext>(options => options
            .UseNpgsql(productsDbConnection)
            .AddInterceptors(softDeleteInterceptor)
        );

        services.AddDbContext<UsersDbContext>(options => options
            .UseNpgsql(usersDbConnection)
            .AddInterceptors(softDeleteInterceptor)
        );

        services.ApplyMigrations();

        return services;
    }

    private static void ApplyMigrations(this IServiceCollection services)
    {
        ProductsDbContext productsDbContext = services.BuildServiceProvider().GetRequiredService<ProductsDbContext>();
        UsersDbContext usersDbContext = services.BuildServiceProvider().GetRequiredService<UsersDbContext>();

        productsDbContext.Database.Migrate();
        usersDbContext.Database.Migrate();
    }
}