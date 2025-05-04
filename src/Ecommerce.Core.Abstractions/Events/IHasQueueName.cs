namespace Ecommerce.Core.Abstractions.Events;

public interface IHasQueueName
{
    public static abstract string QueueName { get; }
}