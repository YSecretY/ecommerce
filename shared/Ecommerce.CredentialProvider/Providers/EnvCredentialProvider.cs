using Ecommerce.CredentialProvider.Credentials;

namespace Ecommerce.CredentialProvider.Providers;

internal class EnvCredentialProvider : ICredentialProvider
{
    public string GetProductsDbConnection() =>
        Environment.GetEnvironmentVariable(CredentialsNames.ProductsDbConnection)
        ?? throw new KeyNotFoundException(nameof(CredentialsNames.ProductsDbConnection));

    public string GetUsersDbConnection() =>
        Environment.GetEnvironmentVariable(CredentialsNames.UsersDbConnection)
        ?? throw new KeyNotFoundException(nameof(CredentialsNames.UsersDbConnection));

    public JwtCredential GetJwtCredential()
    {
        string secret = Environment.GetEnvironmentVariable(CredentialsNames.JwtSecret)
                        ?? throw new KeyNotFoundException(nameof(CredentialsNames.JwtSecret));

        string issuer = Environment.GetEnvironmentVariable(CredentialsNames.JwtIssuer)
                        ?? throw new KeyNotFoundException(nameof(CredentialsNames.JwtIssuer));

        string audience = Environment.GetEnvironmentVariable(CredentialsNames.JwtAudience)
                          ?? throw new KeyNotFoundException(nameof(CredentialsNames.JwtAudience));

        int accessTokenExpirationInMinutes = int.Parse(
            Environment.GetEnvironmentVariable(CredentialsNames.JwtAccessTokenExpirationInMinutes)
            ?? throw new KeyNotFoundException(nameof(CredentialsNames.JwtAccessTokenExpirationInMinutes)));

        int refreshTokenExpirationInDays = int.Parse(
            Environment.GetEnvironmentVariable(CredentialsNames.JwtRefreshTokenExpirationInDays)
            ?? throw new KeyNotFoundException(nameof(CredentialsNames.JwtRefreshTokenExpirationInDays)));

        string cookieName = Environment.GetEnvironmentVariable(CredentialsNames.JwtRefreshTokenCookieName)
                            ?? throw new KeyNotFoundException(nameof(CredentialsNames.JwtRefreshTokenCookieName));

        return new JwtCredential(
            secret: secret,
            issuer: issuer,
            audience: audience,
            accessTokenExpirationMinutes: accessTokenExpirationInMinutes,
            refreshTokenExpirationDays: refreshTokenExpirationInDays,
            refreshTokenCookieName: cookieName);
    }
}