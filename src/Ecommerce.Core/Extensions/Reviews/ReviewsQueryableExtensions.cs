using Ecommerce.Persistence.Domain.Reviews;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Core.Extensions.Reviews;

internal static class ReviewsQueryableExtensions
{
    public static IQueryable<ProductReview> IncludeToSoftDelete(this IQueryable<ProductReview> query) =>
        query
            .AsTracking()
            .Include(r => r.Replies);
}