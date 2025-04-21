namespace Ecommerce.CredentialProvider.Providers;

internal class EnvCredentialProvider : ICredentialProvider
{
    public string GetProductsDbConnection() =>
        Environment.GetEnvironmentVariable(CredentialsNames.ProductsDbConnection)
        ?? throw new KeyNotFoundException(nameof(CredentialsNames.ProductsDbConnection));

    public string GetUsersDbConnection() =>
        Environment.GetEnvironmentVariable(CredentialsNames.UsersDbConnection)
        ?? throw new KeyNotFoundException(nameof(CredentialsNames.UsersDbConnection));
}