namespace Ecommerce.Core.Auth;

public class AuthToken(string accessToken, DateTime expiresAtUtc)
{
    public string AccessToken { get; private set; } = accessToken;

    public DateTime ExpiresAtUtc { get; private set; } = expiresAtUtc;
}