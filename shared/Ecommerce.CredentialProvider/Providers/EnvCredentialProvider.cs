namespace Ecommerce.CredentialProvider.Providers;

internal class EnvCredentialProvider : ICredentialProvider
{
    public string GetProductsDbConnection() =>
        Environment.GetEnvironmentVariable(CredentialsNames.ProductsDbConnection)
        ?? throw new KeyNotFoundException(nameof(CredentialsNames.ProductsDbConnection));
}