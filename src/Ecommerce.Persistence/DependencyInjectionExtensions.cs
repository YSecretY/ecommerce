using Ecommerce.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ecommerce.Persistence;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, string appDbConnection)
    {
        SoftDeleteInterceptor softDeleteInterceptor = new();

        services.TryAddSingleton(softDeleteInterceptor);

        services.AddDbContext<ApplicationDbContext>(options => options
            .UseNpgsql(appDbConnection)
            .AddInterceptors(softDeleteInterceptor)
        );

        services.ApplyMigrations();

        return services;
    }

    private static void ApplyMigrations(this IServiceCollection services)
    {
        ApplicationDbContext applicationDbContext =
            services.BuildServiceProvider().GetRequiredService<ApplicationDbContext>();

        applicationDbContext.Database.Migrate();
    }
}