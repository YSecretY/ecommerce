using System.Security.Claims;
using Ecommerce.Core.Auth.Shared.Internal;
using Ecommerce.Extensions.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Core.Auth.Shared;

internal class IdentityUserAccessor(
    IHttpContextAccessor httpContextAccessor
) : IIdentityUserAccessor
{
    public Guid GetUserId()
    {
        ClaimsPrincipal? user = httpContextAccessor.HttpContext?.User;

        string? userId = user?.FindFirstValue(ClaimsNames.UserId);

        return Guid.Parse(userId ?? throw new UnauthorizedException());
    }
}