namespace Ecommerce.Core.Features.Users.Auth.Register;

public record UserRegisterCommand(string Email, string Password, string FirstName, string LastName);