using Ecommerce.Core.Abstractions.Time;

namespace Ecommerce.Infrastructure.Time;

internal class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;

    public DateOnly UtcToday => DateOnly.FromDateTime(DateTime.UtcNow);
}