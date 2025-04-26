namespace Ecommerce.Infrastructure.Time;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
}