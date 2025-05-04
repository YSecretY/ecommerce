using System.Security.Claims;
using Ecommerce.Core.Abstractions.Auth;
using Ecommerce.Extensions.Exceptions;
using Ecommerce.Infrastructure.Auth.Internal;
using Ecommerce.Persistence.Domain.Users;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Infrastructure.Auth;

internal class IdentityTokenGenerator(
    IJwtHelper jwtHelper,
    JwtSettings jwtSettings,
    IHttpContextAccessor httpContextAccessor
) : IIdentityTokenGenerator
{
    public IdentityToken Generate(User user)
    {
        List<Claim> claims =
        [
            new(ClaimsNames.UserId, user.Id.ToString()),
            new(ClaimsNames.Role, user.Role.ToString())
        ];

        return Generate(claims);
    }

    public IdentityToken Generate(List<Claim> claims)
    {
        HttpContext httpContext = httpContextAccessor.HttpContext
                                  ?? throw new ForbiddenException();

        JwtToken accessToken = jwtHelper.GenerateAccessToken(claims);
        JwtToken refreshToken = jwtHelper.GenerateRefreshToken(claims);

        httpContext.Response.Cookies.Append(jwtSettings.RefreshTokenCookieName, refreshToken.Token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            IsEssential = true,
            Expires = refreshToken.Expiration
        });

        return new IdentityToken(accessToken.Token, accessToken.Expiration);
    }

    public IdentityToken Refresh()
    {
        HttpContext httpContext = httpContextAccessor.HttpContext
                                  ?? throw new ForbiddenException();

        if (!httpContext.Request.Cookies.TryGetValue(jwtSettings.RefreshTokenCookieName, out string? refreshToken))
            throw new ForbiddenException();

        jwtHelper.Validate(refreshToken);

        return Generate(jwtHelper.GetClaimsFromToken(refreshToken).ToList());
    }
}