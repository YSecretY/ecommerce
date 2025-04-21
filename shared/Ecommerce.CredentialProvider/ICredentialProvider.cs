using Ecommerce.CredentialProvider.Credentials;

namespace Ecommerce.CredentialProvider;

public interface ICredentialProvider
{
    public string GetProductsDbConnection();

    public string GetUsersDbConnection();

    public JwtCredential GetJwtCredential();
}