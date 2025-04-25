using Ecommerce.Persistence.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Core.Extensions.Products;

internal static class ProductsQueryableExtensions
{
    public static IQueryable<Product> IncludeToSoftDelete(this IQueryable<Product> query) =>
        query
            .AsTracking()
            .Include(p => p.Reviews)
            .ThenInclude(r => r.Replies);
}