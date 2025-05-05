using System.Text.Json.Serialization;
using Ecommerce.Core.Features.Users.Auth.Register;

namespace Ecommerce.HttpApi.Contracts.Users.Auth.Register;

public class RegisterUserRequest
{
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;

    [JsonPropertyName("firstName")]
    public string FirstName { get; set; } = string.Empty;

    [JsonPropertyName("lastName")]
    public string LastName { get; set; } = string.Empty;

    public UserRegisterCommand ToCommand() => new(
        Email,
        Password,
        FirstName,
        LastName
    );
}