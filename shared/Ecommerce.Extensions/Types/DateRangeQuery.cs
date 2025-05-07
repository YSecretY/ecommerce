namespace Ecommerce.Extensions.Types;

public class DateRangeQuery
{
    public DateRangeQuery(DateOnly from, DateOnly to)
    {
        ArgumentOutOfRangeException.ThrowIfEqual(from, to);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(from, to);

        From = from;
        To = to;
    }

    public DateOnly From { get; private set; }

    public DateOnly To { get; private set; }
}