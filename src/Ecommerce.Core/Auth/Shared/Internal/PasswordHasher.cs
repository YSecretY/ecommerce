namespace Ecommerce.Core.Auth.Shared.Internal;

internal class PasswordHasher : IPasswordHasher
{
    public string Hash(string password) =>
        BCrypt.Net.BCrypt.HashPassword(password);

    public bool IsValid(string password, string hash) =>
        BCrypt.Net.BCrypt.Verify(password, hash);
}