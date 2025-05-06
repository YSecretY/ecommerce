namespace Ecommerce.Core.Abstractions.Time;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
}