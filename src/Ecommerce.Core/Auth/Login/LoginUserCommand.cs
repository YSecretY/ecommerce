using Ecommerce.Domain.Users;
using Ecommerce.Extensions.Exceptions;

namespace Ecommerce.Core.Auth.Login;

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