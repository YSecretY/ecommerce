namespace Ecommerce.Core.Abstractions.Auth;

public interface IPasswordHasher
{
    public string Hash(string password);

    public bool IsValid(string password, string hash);
}