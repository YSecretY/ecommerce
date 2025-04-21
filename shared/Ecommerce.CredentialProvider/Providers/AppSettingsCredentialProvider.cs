using Microsoft.Extensions.Configuration;

namespace Ecommerce.CredentialProvider.Providers;

internal class AppSettingsCredentialProvider(
    IConfiguration configuration
) : ICredentialProvider
{
    public string GetProductsDbConnection() =>
        configuration.GetConnectionString(CredentialsNames.ProductsDbConnection)
        ?? throw new KeyNotFoundException(nameof(CredentialsNames.ProductsDbConnection));

    public string GetUsersDbConnection() =>
        configuration.GetConnectionString(CredentialsNames.UsersDbConnection)
        ?? throw new KeyNotFoundException(nameof(CredentialsNames.UsersDbConnection));
}