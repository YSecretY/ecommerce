using Ecommerce.Infrastructure.Database;
using Ecommerce.Infrastructure.Repositories.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ecommerce.Infrastructure;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string productsDbConnection)
    {
        services.AddDbContext<ProductsDbContext>(options => options.UseNpgsql(productsDbConnection));

        services.ApplyMigrations();

        services.AddRepositories();

        return services;
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.TryAddScoped<IProductsRepository, ProductsRepository>();
    }

    private static void ApplyMigrations(this IServiceCollection services)
    {
        ProductsDbContext dbContext = services.BuildServiceProvider().GetRequiredService<ProductsDbContext>();

        dbContext.Database.Migrate();
    }
}