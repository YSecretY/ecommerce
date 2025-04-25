namespace Ecommerce.Core.Features.Auth.Shared.Internal;

internal interface IPasswordHasher
{
    public string Hash(string password);

    public bool IsValid(string password, string hash);
}