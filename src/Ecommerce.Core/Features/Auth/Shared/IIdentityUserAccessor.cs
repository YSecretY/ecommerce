namespace Ecommerce.Core.Features.Auth.Shared;

public interface IIdentityUserAccessor
{
    public Guid GetUserId();
}