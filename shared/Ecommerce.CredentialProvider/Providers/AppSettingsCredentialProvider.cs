using Ecommerce.CredentialProvider.Credentials;
using Microsoft.Extensions.Configuration;

namespace Ecommerce.CredentialProvider.Providers;

internal class AppSettingsCredentialProvider(
    IConfiguration configuration
) : ICredentialProvider
{
    public string GetAppDbConnection() =>
        configuration.GetConnectionString(nameof(CredentialsNames.AppDbConnection))
        ?? throw new KeyNotFoundException(nameof(CredentialsNames.AppDbConnection));

    public JwtCredential GetJwtCredential() =>
        configuration.GetSection(nameof(JwtCredential)).Get<JwtCredential>()
        ?? throw new KeyNotFoundException(nameof(JwtCredential));

    public KafkaCredential GetKafkaCredential() =>
        configuration.GetSection(nameof(KafkaCredential)).Get<KafkaCredential>()
        ?? throw new KeyNotFoundException(nameof(KafkaCredential));

    public MongoDbCredential GetMongoDbCredential() =>
        configuration.GetSection(nameof(MongoDbCredential)).Get<MongoDbCredential>()
        ?? throw new KeyNotFoundException(nameof(MongoDbCredential));
}