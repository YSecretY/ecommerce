namespace Ecommerce.Extensions.Types;

public class PaginatedEnumerable<T>
{
    public PaginatedEnumerable(
        IEnumerable<T> data,
        int pageSize,
        int pageNumber,
        long totalCount
    )
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pageSize, nameof(pageSize));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pageNumber, nameof(pageNumber));

        Data = data;
        PageSize = pageSize;
        PageNumber = pageNumber;
        TotalCount = totalCount;
    }

    public PaginatedEnumerable<TOther> Map<TOther>(Func<T, TOther> map) =>
        new(Data.Select(map), PageSize, PageNumber, TotalCount);

    public IEnumerable<T> Data { get; private set; }

    public int PageSize { get; private set; }

    public int PageNumber { get; private set; }

    public long TotalCount { get; private set; }

    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}