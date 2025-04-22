using Ecommerce.Extensions.Types;

namespace Ecommerce.HttpApi.Contracts;

public class PaginatedResult<T>
{
    public PaginatedResult(
        IEnumerable<T> data,
        int pageSize,
        int pageNumber,
        int totalCount
    )
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pageSize, nameof(pageSize));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pageNumber, nameof(pageNumber));

        Data = data;
        PageSize = pageSize;
        PageNumber = pageNumber;
        TotalCount = totalCount;
    }

    public PaginatedResult(PaginatedEnumerable<T> paginatedEnumerable)
    {
        Data = paginatedEnumerable.Data;
        PageSize = paginatedEnumerable.PageSize;
        PageNumber = paginatedEnumerable.PageNumber;
        TotalCount = paginatedEnumerable.TotalCount;
    }

    public IEnumerable<T> Data { get; private set; }

    public int PageSize { get; private set; }

    public int PageNumber { get; private set; }

    public int TotalCount { get; private set; }
}