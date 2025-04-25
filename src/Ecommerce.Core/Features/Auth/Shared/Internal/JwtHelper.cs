using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ecommerce.Extensions.Exceptions;
using Ecommerce.Extensions.Time;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Core.Features.Auth.Shared.Internal;

internal class JwtHelper : IJwtHelper
{
    private readonly JwtSettings _jwtSettings;
    private readonly SymmetricSecurityKey _securityKey;
    private readonly SigningCredentials _signingCredentials;
    private readonly JwtSecurityTokenHandler _handler = new();

    private readonly IDateTimeProvider _dateTimeProvider;

    public JwtHelper(JwtSettings jwtSettings, IDateTimeProvider dateTimeProvider)
    {
        _jwtSettings = jwtSettings ?? throw new ArgumentNullException(nameof(jwtSettings));
        _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        _signingCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);
    }

    public JwtToken GenerateAccessToken(IEnumerable<Claim> claims)
    {
        DateTime expiration = _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes);

        JwtSecurityToken token = new(
            issuer: _jwtSettings.Issuer,
            expires: expiration,
            audience: _jwtSettings.Audience,
            claims: claims,
            signingCredentials: _signingCredentials
        );

        return new JwtToken(token: _handler.WriteToken(token), expiration: expiration);
    }

    public JwtToken GenerateRefreshToken(IEnumerable<Claim> claims)
    {
        DateTime expiration = _dateTimeProvider.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);

        JwtSecurityToken token = new(
            issuer: _jwtSettings.Issuer,
            expires: expiration,
            audience: _jwtSettings.Audience,
            claims: claims,
            signingCredentials: _signingCredentials
        );

        return new JwtToken(token: _handler.WriteToken(token), expiration: expiration);
    }

    public void Validate(string refreshToken)
    {
        try
        {
            TokenValidationParameters parameters = new()
            {
                ValidateIssuer = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _securityKey,
                ValidateAudience = true,
                ValidAudience = _jwtSettings.Audience
            };

            _ = _handler.ValidateToken(refreshToken, parameters, out _);
        }
        catch
        {
            throw new UnauthorizedException();
        }
    }

    public IEnumerable<Claim> GetClaimsFromToken(string token) =>
        _handler.ReadJwtToken(token).Claims;
}