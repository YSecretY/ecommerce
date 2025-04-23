namespace Ecommerce.Core.Auth.Shared;

public interface IIdentityUserAccessor
{
    public Guid GetUserId();
}