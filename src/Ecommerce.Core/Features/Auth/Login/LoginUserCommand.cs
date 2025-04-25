using Ecommerce.Extensions.Exceptions;
using Ecommerce.Persistence.Domain.Users;

namespace Ecommerce.Core.Features.Auth.Login;

public class LoginUserCommand
{
    public LoginUserCommand(string email, string password)
    {
        UnauthorizedException.ThrowIf(string.IsNullOrWhiteSpace(email) || email.Length > User.MaxEmailLength);
        UnauthorizedException.ThrowIf(string.IsNullOrWhiteSpace(password) || password.Length > User.MaxPasswordLength);

        Email = email;
        Password = password;
    }

    public string Email { get; private set; }

    public string Password { get; private set; }
}