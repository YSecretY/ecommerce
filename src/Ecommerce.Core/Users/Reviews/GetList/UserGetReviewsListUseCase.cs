using Ecommerce.Core.Auth.Shared;
using Ecommerce.Extensions.Types;
using Ecommerce.Persistence.Database;
using Ecommerce.Persistence.Domain.Reviews;
using Ecommerce.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Core.Users.Reviews.GetList;

public class UserGetReviewsListUseCase(
    ProductsDbContext dbContext,
    IIdentityUserAccessor identityUserAccessor
) : IUserGetReviewsListUseCase
{
    public async Task<PaginatedEnumerable<ProductReviewDto>> HandleAsync(PaginationQuery paginationQuery,
        CancellationToken cancellationToken = default)
    {
        Guid userId = identityUserAccessor.GetUserId();

        PaginatedEnumerable<ProductReview> reviews = await dbContext.ProductsReviews
            .AsNoTracking()
            .Where(r => r.UserId == userId)
            .ToPaginatedEnumerableAsync(paginationQuery, cancellationToken);

        return reviews.Map(r => new ProductReviewDto(r));
    }
}