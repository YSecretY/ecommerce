using System.Security.Claims;

namespace Ecommerce.Infrastructure.Auth.Internal;

internal interface IJwtHelper
{
    public JwtToken GenerateAccessToken(IEnumerable<Claim> claims);

    public JwtToken GenerateRefreshToken(IEnumerable<Claim> claims);

    public void Validate(string refreshToken);

    public IEnumerable<Claim> GetClaimsFromToken(string token);
}