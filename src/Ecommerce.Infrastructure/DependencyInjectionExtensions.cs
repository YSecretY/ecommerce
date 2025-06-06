using System.Text;
using Ecommerce.Core.Abstractions.Analytics;
using Ecommerce.Core.Abstractions.Analytics.Services;
using Ecommerce.Core.Abstractions.Auth;
using Ecommerce.Core.Abstractions.Events;
using Ecommerce.Core.Abstractions.Events.Orders;
using Ecommerce.Core.Abstractions.Events.Products;
using Ecommerce.Core.Abstractions.Time;
using Ecommerce.Infrastructure.Analytics.Internal.EventHandlers;
using Ecommerce.Infrastructure.Analytics.Internal.Mongo.Services;
using Ecommerce.Infrastructure.Analytics.Internal.Mongo.Services.Writers;
using Ecommerce.Infrastructure.Auth;
using Ecommerce.Infrastructure.Auth.Internal;
using Ecommerce.Infrastructure.Events.Internal;
using Ecommerce.Infrastructure.Events.Internal.Consumers;
using Ecommerce.Infrastructure.Events.Internal.KafkaProducers;
using Ecommerce.Infrastructure.Events.Internal.Services;
using Ecommerce.Infrastructure.Mongo;
using Ecommerce.Infrastructure.Mongo.Internal;
using Ecommerce.Infrastructure.Time;
using Ecommerce.Kafka;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Infrastructure;

public static class DependencyInjectionExtensions
{
    public static async Task AddInfrastructure(
        this IServiceCollection services,
        JwtSettings jwtSettings,
        string kafkaBootstrapServers,
        MongoDbSettings mongoDbSettings
    )
    {
        services
            .AddTime()
            .AddAuth(jwtSettings)
            .AddAnalytics();

        await services.AddEvents(new KafkaSettings([
            new TopicSettings(ProductViewedEvent.QueueName, 3, 1)
        ], kafkaBootstrapServers));

        await services.AddMongoDb(mongoDbSettings);
    }

    private static IServiceCollection AddTime(this IServiceCollection services)
    {
        services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }

    private static IServiceCollection AddAuth(this IServiceCollection services, JwtSettings jwtSettings)
    {
        services.AddHttpContextAccessor();
        services.TryAddSingleton<IPasswordHasher, PasswordHasher>();
        services.TryAddSingleton(jwtSettings);
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

    private static async Task AddEvents(this IServiceCollection services, KafkaSettings kafkaSettings)
    {
        await services.AddKafka(kafkaSettings);

        services.TryAddSingleton<IKafkaEventProducer<ProductViewedEvent>, ProductViewedEventProducer>();
        services.TryAddSingleton<IKafkaEventProducer<OrderCreatedEvent>, OrderCreatedEventProducer>();

        services.TryAddSingleton<IEventPublisher, KafkaEventPublisher>();
        services.TryAddSingleton<IEventsInfoService, EventsInfoService>();

        services.AddHostedService<ProductViewedEventConsumer>();
        services.AddHostedService<OrderCreatedEventConsumer>();
    }

    private static IServiceCollection AddAnalytics(this IServiceCollection services)
    {
        services.TryAddSingleton<IAnalyticsEventHandler<OrderCreatedEvent>, AnalyticsOrderCreatedEventHandler>();
        services.TryAddSingleton<IAnalyticsEventHandler<ProductViewedEvent>, AnalyticsProductViewedEventHandler>();

        services.TryAddSingleton<IAnalyticsProductsService, AnalyticsProductsService>();
        services.TryAddSingleton<IAnalyticsUserService, AnalyticsUserService>();

        services.TryAddSingleton<IProductStatisticsWriter, ProductStatisticsWriter>();
        services.TryAddSingleton<IUserProductViewsStatisticsWriter, UserProductViewsStatisticsWriter>();
        services.TryAddSingleton<IOrderStatisticsWriter, OrderStatisticsWriter>();

        return services;
    }

    private static async Task AddMongoDb(this IServiceCollection services, MongoDbSettings settings)
    {
        services.TryAddSingleton(settings);

        services.TryAddSingleton<MongoDbContext>();

        services.TryAddSingleton<MongoDbMigrationService>();

        MongoDbMigrationService migrationService =
            services.BuildServiceProvider().GetRequiredService<MongoDbMigrationService>();

        await migrationService.RunMigrationsAsync();
    }
}