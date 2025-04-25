using Ecommerce.Extensions.Types;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Persistence.Extensions;

public static class QueryableExtensions
{
    public static async Task<PaginatedEnumerable<T>> ToPaginatedEnumerableAsync<T>(
        this IQueryable<T> query,
        PaginationQuery paginationQuery,
        CancellationToken cancellationToken = default
    )
    {
        ArgumentNullException.ThrowIfNull(query);
        ArgumentNullException.ThrowIfNull(paginationQuery);

        int totalCount = await query.CountAsync(cancellationToken);

        List<T> data = await query
            .Skip((paginationQuery.PageNumber - 1) * paginationQuery.PageSize)
            .Take(paginationQuery.PageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedEnumerable<T>(data, paginationQuery.PageSize, paginationQuery.PageNumber, totalCount);
    }
}