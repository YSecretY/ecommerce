using Ecommerce.Core;
using Ecommerce.CredentialProvider;
using Ecommerce.CredentialProvider.Credentials;
using Ecommerce.Extensions;
using Ecommerce.HttpApi.Extensions;
using Ecommerce.Infrastructure;
using Ecommerce.Infrastructure.Auth;
using Ecommerce.Infrastructure.Mongo;
using Ecommerce.Persistence;
using Microsoft.OpenApi.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

# region Credentials

ICredentialProvider credentialProvider =
    CredentialProviderFactory.GetCredentialProvider(builder.Environment, builder.Configuration);

string appDbConnection = credentialProvider.GetAppDbConnection();
JwtCredential jwtCredential = credentialProvider.GetJwtCredential();
KafkaCredential kafkaCredential = credentialProvider.GetKafkaCredential();
MongoDbCredential mongoDbCredential = credentialProvider.GetMongoDbCredential();

# endregion

# region Core

builder.Services
    .AddExtensions()
    .AddPersistence(appDbConnection);

await builder.Services.AddInfrastructure(
    new JwtSettings(
        secret: jwtCredential.Secret,
        issuer: jwtCredential.Issuer,
        audience: jwtCredential.Audience,
        accessTokenExpirationMinutes: jwtCredential.AccessTokenExpirationMinutes,
        refreshTokenExpirationDays: jwtCredential.RefreshTokenExpirationDays,
        refreshTokenCookieName: jwtCredential.RefreshTokenCookieName
    ),
    kafkaCredential.BootstrapServers,
    new MongoDbSettings(
        connectionString: mongoDbCredential.ConnectionString,
        databaseName: mongoDbCredential.DatabaseName
    )
);

builder.Services
    .AddCore()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Enter your Bearer token"
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
    });

# endregion

WebApplication app = builder.Build();

app.MapAuthEndpoints();
app.MapProductsEndpoints();
app.MapReviewsEndpoints();
app.MapRepliesEndpoints();
app.MapAdminEndpoints();
app.MapOrdersEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.Run();