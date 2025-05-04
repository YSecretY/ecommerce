namespace Ecommerce.Infrastructure.Events.Internal.Consumers;

internal interface IHasGroupId
{
    public static abstract string GroupId { get; }
}