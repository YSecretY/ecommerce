using Ecommerce.Persistence.Domain.Users;

namespace Ecommerce.Infrastructure.Auth.Abstractions;

public interface IIdentityUserAccessor
{
    public Guid GetUserId();

    public bool IsAuthenticated();

    public bool IsInRole(UserRole role);
}