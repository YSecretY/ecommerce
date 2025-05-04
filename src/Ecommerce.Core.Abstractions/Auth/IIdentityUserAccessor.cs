using Ecommerce.Persistence.Domain.Users;

namespace Ecommerce.Core.Abstractions.Auth;

public interface IIdentityUserAccessor
{
    public Guid GetUserId();

    public bool IsAuthenticated();

    public bool IsInRole(UserRole role);
}