namespace Ecommerce.Infrastructure.Auth.Abstractions;

internal struct JwtToken(string token, DateTime expiration)
{
    public string Token { get; private set; } = token;

    public DateTime Expiration { get; private set; } = expiration;
}