using System.Security.Claims;

namespace Ecommerce.Core.Features.Auth.Shared.Internal;

internal interface IJwtHelper
{
    public JwtToken GenerateAccessToken(IEnumerable<Claim> claims);

    public JwtToken GenerateRefreshToken(IEnumerable<Claim> claims);

    public void Validate(string refreshToken);

    public IEnumerable<Claim> GetClaimsFromToken(string token);
}