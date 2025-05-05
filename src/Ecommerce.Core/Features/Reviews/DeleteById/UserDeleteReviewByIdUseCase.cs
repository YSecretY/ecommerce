using Ecommerce.Core.Abstractions.Auth;
using Ecommerce.Core.Exceptions.Reviews;
using Ecommerce.Core.Extensions.Reviews;
using Ecommerce.Extensions.Exceptions;
using Ecommerce.Persistence.Database;
using Ecommerce.Persistence.Domain.Reviews;
using Ecommerce.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Core.Features.Reviews.DeleteById;

public class UserDeleteReviewByIdUseCase(
    ApplicationDbContext dbContext,
    IIdentityUserAccessor identityUserAccessor
) : IUserDeleteReviewByIdUseCase
{
    public async Task HandleAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Guid userId = identityUserAccessor.GetUserId();

        ProductReview review = await dbContext.ProductsReviews
                                   .IncludeToSoftDelete()
                                   .FirstOrDefaultAsync(r => r.Id == id, cancellationToken)
                               ?? throw new ProductReviewNotFoundException();

        if (review.UserId != userId)
            throw new ForbiddenException();

        dbContext.SoftDelete(review);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}