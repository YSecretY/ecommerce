using System.Text.Json.Serialization;
using Ecommerce.Core.Features.Auth.Shared;

namespace Ecommerce.HttpApi.Contracts.Auth;

public class IdentityTokenResponse(string accessToken, DateTime expiresAtUtc)
{
    public IdentityTokenResponse(IdentityToken token) : this(token.AccessToken, token.ExpiresAtUtc)
    {
    }

    [JsonPropertyName("accessToken")]
    public string AccessToken { get; private set; } = accessToken;

    [JsonPropertyName("expiresAtUtc")]
    public DateTime ExpiresAtUtc { get; private set; } = expiresAtUtc;
}