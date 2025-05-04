using Ecommerce.Core.Abstractions.Auth;

namespace Ecommerce.Infrastructure.Auth.Internal;

internal class PasswordHasher : IPasswordHasher
{
    public string Hash(string password) =>
        BCrypt.Net.BCrypt.HashPassword(password);

    public bool IsValid(string password, string hash) =>
        BCrypt.Net.BCrypt.Verify(password, hash);
}