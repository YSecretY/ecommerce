namespace Ecommerce.Infrastructure.Auth.Abstractions;

public interface IIdentityUserAccessor
{
    public Guid GetUserId();
}