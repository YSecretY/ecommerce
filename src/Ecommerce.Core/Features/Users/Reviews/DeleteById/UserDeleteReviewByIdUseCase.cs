using Ecommerce.Core.Exceptions.Reviews;
using Ecommerce.Core.Features.Auth.Shared;
using Ecommerce.Extensions.Exceptions;
using Ecommerce.Persistence.Database;
using Ecommerce.Persistence.Domain.Reviews;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Core.Features.Users.Reviews.DeleteById;

public class UserDeleteReviewByIdUseCase(
    ProductsDbContext dbContext,
    IIdentityUserAccessor identityUserAccessor
) : IUserDeleteReviewByIdUseCase
{
    public async Task HandleAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Guid userId = identityUserAccessor.GetUserId();

        ProductReview review = await dbContext.ProductsReviews
                                   .AsNoTracking()
                                   .FirstOrDefaultAsync(r => r.Id == id, cancellationToken)
                               ?? throw new ProductReviewNotFoundException();

        if (review.UserId != userId)
            throw new UnauthorizedException();

        dbContext.ProductsReviews.Remove(review);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}