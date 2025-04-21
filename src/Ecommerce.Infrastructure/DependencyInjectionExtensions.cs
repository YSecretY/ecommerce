using Ecommerce.Infrastructure.Database;
using Ecommerce.Infrastructure.Database.Products;
using Ecommerce.Infrastructure.Database.Users;
using Ecommerce.Infrastructure.Repositories.Products;
using Ecommerce.Infrastructure.Repositories.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ecommerce.Infrastructure;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string productsDbConnection,
        string usersDbConnection)
    {
        services.AddDbContext<ProductsDbContext>(options => options.UseNpgsql(productsDbConnection));
        services.AddDbContext<UsersDbContext>(options => options.UseNpgsql(usersDbConnection));

        services.ApplyMigrations();

        services.AddRepositories();
        services.AddUnitsOfWork();

        return services;
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.TryAddScoped<IProductsRepository, ProductsRepository>();
        services.TryAddScoped<IUsersRepository, UsersRepository>();
    }

    private static void AddUnitsOfWork(this IServiceCollection services)
    {
        services.TryAddScoped<IProductsUnitOfWork, ProductsUnitOfWork>();
        services.TryAddScoped<IUsersUnitOfWork, UsersUnitOfWork>();
    }

    private static void ApplyMigrations(this IServiceCollection services)
    {
        ProductsDbContext productsDbContext = services.BuildServiceProvider().GetRequiredService<ProductsDbContext>();
        UsersDbContext usersDbContext = services.BuildServiceProvider().GetRequiredService<UsersDbContext>();

        productsDbContext.Database.Migrate();
        usersDbContext.Database.Migrate();
    }
}