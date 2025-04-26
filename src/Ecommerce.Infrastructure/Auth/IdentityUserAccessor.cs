using System.Security.Claims;
using Ecommerce.Extensions.Exceptions;
using Ecommerce.Infrastructure.Auth.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Infrastructure.Auth;

internal class IdentityUserAccessor(
    IHttpContextAccessor httpContextAccessor
) : IIdentityUserAccessor
{
    public Guid GetUserId()
    {
        ClaimsPrincipal? user = httpContextAccessor.HttpContext?.User;

        string? userId = user?.FindFirstValue(ClaimsNames.UserId);

        return Guid.Parse(userId ?? throw new ForbiddenException());
    }
}