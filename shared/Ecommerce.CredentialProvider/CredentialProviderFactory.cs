using Ecommerce.CredentialProvider.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Ecommerce.CredentialProvider;

public static class CredentialProviderFactory
{
    public static ICredentialProvider GetCredentialProvider(IHostEnvironment environment, IConfiguration configuration) =>
        environment.IsDevelopment()
            ? new AppSettingsCredentialProvider(configuration)
            : new EnvCredentialProvider();
}