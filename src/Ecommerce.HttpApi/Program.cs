using Ecommerce.Core;
using Ecommerce.Core.Features.Auth;
using Ecommerce.CredentialProvider;
using Ecommerce.CredentialProvider.Credentials;
using Ecommerce.Extensions;
using Ecommerce.Persistence;
using Microsoft.OpenApi.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

# region Credentials

ICredentialProvider credentialProvider =
    CredentialProviderFactory.GetCredentialProvider(builder.Environment, builder.Configuration);

string productsDbConnection = credentialProvider.GetProductsDbConnection();
string usersDbConnection = credentialProvider.GetUsersDbConnection();
JwtCredential jwtCredential = credentialProvider.GetJwtCredential();

# endregion

# region Core

builder.Services
    .AddExtensions()
    .AddPersistence(productsDbConnection, usersDbConnection)
    .AddCore(new JwtSettings(
        secret: jwtCredential.Secret,
        issuer: jwtCredential.Issuer,
        audience: jwtCredential.Audience,
        accessTokenExpirationMinutes: jwtCredential.AccessTokenExpirationMinutes,
        refreshTokenExpirationDays: jwtCredential.RefreshTokenExpirationDays,
        refreshTokenCookieName: jwtCredential.RefreshTokenCookieName
    ))
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
    })
    .AddControllers();

# endregion

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();