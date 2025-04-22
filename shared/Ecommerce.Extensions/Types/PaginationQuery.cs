namespace Ecommerce.Extensions.Types;

public class PaginationQuery
{
    public PaginationQuery(int pageSize, int pageNumber)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pageSize, nameof(pageSize));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pageNumber, nameof(pageNumber));

        PageSize = pageSize;
        PageNumber = pageNumber;
    }

    public int PageSize { get; private set; }

    public int PageNumber { get; private set; }
}