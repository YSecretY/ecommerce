using System.Text;
using Ecommerce.Analytics.Events.Products;
using Ecommerce.Infrastructure.Auth;
using Ecommerce.Infrastructure.Auth.Abstractions;
using Ecommerce.Infrastructure.Events;
using Ecommerce.Infrastructure.Events.Internal;
using Ecommerce.Infrastructure.Events.Internal.KafkaProducers;
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
        string kafkaBootstrapServers
    )
    {
        services
            .AddTime()
            .AddAuth(jwtSettings);

        await services.AddEvents(new KafkaSettings([
            new TopicSettings(Topics.ProductViewed, 3, 1)
        ], kafkaBootstrapServers));
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

        services.TryAddSingleton<IEventPublisher, KafkaEventPublisher>();
    }
}