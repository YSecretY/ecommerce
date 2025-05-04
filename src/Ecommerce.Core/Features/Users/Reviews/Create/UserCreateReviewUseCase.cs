using Ecommerce.Core.Abstractions.Auth;
using Ecommerce.Infrastructure.Time;
using Ecommerce.Persistence.Database;
using Ecommerce.Persistence.Domain.Reviews;

namespace Ecommerce.Core.Features.Users.Reviews.Create;

public class UserCreateReviewUseCase(
    ApplicationDbContext dbContext,
    IDateTimeProvider dateTimeProvider,
    IIdentityUserAccessor identityUserAccessor
) : IUserCreateReviewUseCase
{
    public async Task<Guid> HandleAsync(UserCreateReviewCommand command, CancellationToken cancellationToken = default)
    {
        ProductReview review = ProductReviewValidator.CreateValid(
            userId: identityUserAccessor.GetUserId(),
            productId: command.ProductId,
            text: command.Text,
            createdAtUtc: dateTimeProvider.UtcNow
        );

        await dbContext.ProductsReviews.AddAsync(review, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return review.Id;
    }
}