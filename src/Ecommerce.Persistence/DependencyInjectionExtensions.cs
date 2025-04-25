using Ecommerce.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Persistence;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, string productsDbConnection,
        string usersDbConnection)
    {
        services.AddDbContext<ProductsDbContext>(options => options.UseNpgsql(productsDbConnection));
        services.AddDbContext<UsersDbContext>(options => options.UseNpgsql(usersDbConnection));

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