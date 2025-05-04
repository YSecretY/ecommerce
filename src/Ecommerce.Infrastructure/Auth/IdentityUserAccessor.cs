using System.Security.Claims;
using Ecommerce.Core.Abstractions.Auth;
using Ecommerce.Extensions.Exceptions;
using Ecommerce.Infrastructure.Auth.Internal;
using Ecommerce.Persistence.Domain.Users;
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

    public bool IsAuthenticated() =>
        httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;

    public bool IsInRole(UserRole role)
    {
        ClaimsPrincipal? user = httpContextAccessor.HttpContext?.User;

        return user?.IsInRole(role.ToString()) ?? false;
    }
}