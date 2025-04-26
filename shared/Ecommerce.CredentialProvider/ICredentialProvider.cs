using Ecommerce.CredentialProvider.Credentials;

namespace Ecommerce.CredentialProvider;

public interface ICredentialProvider
{
    public string GetAppDbConnection();

    public JwtCredential GetJwtCredential();
}