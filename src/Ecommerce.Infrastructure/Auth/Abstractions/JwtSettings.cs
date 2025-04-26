namespace Ecommerce.Infrastructure.Auth.Abstractions;

public class JwtSettings
{
    public JwtSettings(string secret, string issuer, string audience, int accessTokenExpirationMinutes,
        int refreshTokenExpirationDays, string refreshTokenCookieName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(secret, nameof(secret));
        ArgumentException.ThrowIfNullOrWhiteSpace(issuer, nameof(issuer));
        ArgumentException.ThrowIfNullOrWhiteSpace(audience, nameof(audience));
        ArgumentException.ThrowIfNullOrWhiteSpace(refreshTokenCookieName, nameof(refreshTokenCookieName));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(accessTokenExpirationMinutes,
            nameof(accessTokenExpirationMinutes));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(refreshTokenExpirationDays, nameof(refreshTokenExpirationDays));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(refreshTokenExpirationDays, 31, nameof(refreshTokenExpirationDays));

        Secret = secret;
        Issuer = issuer;
        Audience = audience;
        AccessTokenExpirationMinutes = accessTokenExpirationMinutes;
        RefreshTokenExpirationDays = refreshTokenExpirationDays;
        RefreshTokenCookieName = refreshTokenCookieName;
    }

    public string Secret { get; private set; }

    public string Issuer { get; private set; }

    public string Audience { get; private set; }

    public int AccessTokenExpirationMinutes { get; private set; }

    public int RefreshTokenExpirationDays { get; private set; }

    public string RefreshTokenCookieName { get; private set; }
}