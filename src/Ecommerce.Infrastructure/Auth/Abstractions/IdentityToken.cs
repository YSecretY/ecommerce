namespace Ecommerce.Infrastructure.Auth.Abstractions;

public class IdentityToken(string accessToken, DateTime expiresAtUtc)
{
    internal IdentityToken(JwtToken jwtToken) : this(jwtToken.Token, jwtToken.Expiration)
    {
    }

    public string AccessToken { get; private set; } = accessToken;

    public DateTime ExpiresAtUtc { get; private set; } = expiresAtUtc;
}