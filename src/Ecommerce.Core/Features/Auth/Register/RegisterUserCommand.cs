namespace Ecommerce.Core.Features.Auth.Register;

public record RegisterUserCommand(string Email, string Password, string FirstName, string LastName);