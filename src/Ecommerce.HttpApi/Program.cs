using Ecommerce.Core;
using Ecommerce.Core.Auth;
using Ecommerce.CredentialProvider;
using Ecommerce.CredentialProvider.Credentials;
using Ecommerce.Extensions;
using Ecommerce.Infrastructure;

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
    .AddInfrastructure(productsDbConnection, usersDbConnection)
    .AddCore(new JwtSettings(
        secret: jwtCredential.Secret,
        issuer: jwtCredential.Issuer,
        audience: jwtCredential.Audience,
        accessTokenExpirationMinutes: jwtCredential.AccessTokenExpirationMinutes,
        refreshTokenExpirationDays: jwtCredential.RefreshTokenExpirationDays,
        refreshTokenCookieName: jwtCredential.RefreshTokenCookieName
    ))
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
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