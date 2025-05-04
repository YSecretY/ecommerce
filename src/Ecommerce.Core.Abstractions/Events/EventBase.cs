namespace Ecommerce.Core.Abstractions.Events;

public class EventBase(DateTime occuredAtUtc)
{
    public Guid EventId { get; private init; } = Guid.NewGuid();

    public DateTime OccuredAtUtc { get; private init; } = occuredAtUtc;
}