using System.Security.Claims;
using Ecommerce.Core.Features.Auth.Shared.Internal;
using Ecommerce.Extensions.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Core.Features.Auth.Shared;

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