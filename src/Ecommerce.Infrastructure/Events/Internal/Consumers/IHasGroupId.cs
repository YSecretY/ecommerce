namespace Ecommerce.Infrastructure.Events.Internal.Consumers;

public interface IHasGroupId
{
    public static abstract string GroupId { get; }
}