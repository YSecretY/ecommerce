namespace Ecommerce.Core.Abstractions.Time;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
    
    public DateOnly UtcToday { get; }
}