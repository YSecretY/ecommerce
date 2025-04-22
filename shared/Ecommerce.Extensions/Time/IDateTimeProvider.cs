namespace Ecommerce.Extensions.Time;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
}