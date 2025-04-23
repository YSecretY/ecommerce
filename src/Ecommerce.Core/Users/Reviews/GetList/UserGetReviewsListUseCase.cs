using Ecommerce.Core.Auth.Shared;
using Ecommerce.Domain.Reviews;
using Ecommerce.Extensions.Types;
using Ecommerce.Infrastructure.Repositories.Reviews;

namespace Ecommerce.Core.Users.Reviews.GetList;

public class UserGetReviewsListUseCase(
    IProductsReviewsRepository reviewsRepository,
    IIdentityUserAccessor identityUserAccessor
) : IUserGetReviewsListUseCase
{
    public async Task<PaginatedEnumerable<ProductReviewDto>> HandleAsync(PaginationQuery paginationQuery,
        CancellationToken cancellationToken = default)
    {
        Guid userId = identityUserAccessor.GetUserId();

        PaginatedEnumerable<ProductReview> reviews = await reviewsRepository.GetListAsync(
            paginationQuery,
            filter: query => query.Where(r => r.UserId == userId),
            cancellationToken: cancellationToken
        );

        return reviews.Map(r => new ProductReviewDto(r));
    }
}