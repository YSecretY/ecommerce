namespace Ecommerce.CredentialProvider;

public class CredentialsNames
{
    public const string AppDbConnection = "AppDbConnection";

    public const string JwtSecret = "JwtSecret";
    public const string JwtIssuer = "JwtIssuer";
    public const string JwtAudience = "JwtAudience";
    public const string JwtAccessTokenExpirationInMinutes = "JwtAccessTokenExpirationInMinutes";
    public const string JwtRefreshTokenExpirationInDays = "JwtRefreshTokenExpirationInMinutes";
    public const string JwtRefreshTokenCookieName = "JwtRefreshTokenCookieName";
    
    public const string KafkaBootstrapServers = "KafkaBootstrapServers";
}