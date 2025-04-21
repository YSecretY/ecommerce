namespace Ecommerce.Core.Auth.Register.Internal;

internal interface IPasswordHasher
{
    public string Hash(string password);

    public bool Verify(string password, string hash);
}