using System.Text.Json.Serialization;
using Ecommerce.Core.Features.Users.Auth.Login;

namespace Ecommerce.HttpApi.Contracts.Users.Auth.Login;

public class LoginUserRequest
{
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;

    public UserLoginCommand ToCommand() =>
        new(Email, Password);
}