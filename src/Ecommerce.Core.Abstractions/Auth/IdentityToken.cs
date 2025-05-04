namespace Ecommerce.Core.Abstractions.Auth;

public class IdentityToken(string accessToken, DateTime expiresAtUtc)
{
    public string AccessToken { get; private set; } = accessToken;

    public DateTime ExpiresAtUtc { get; private set; } = expiresAtUtc;
}