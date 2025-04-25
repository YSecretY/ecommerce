using Ecommerce.Core.Features.Auth.Shared;
using Ecommerce.Extensions.Exceptions;
using Ecommerce.Extensions.Time;
using Ecommerce.Persistence.Database;
using Ecommerce.Persistence.Domain.Reviews;

namespace Ecommerce.Core.Features.Users.Reviews.Create;

public class UserCreateReviewUseCase(
    ProductsDbContext dbContext,
    IDateTimeProvider dateTimeProvider,
    IIdentityUserAccessor identityUserAccessor
) : IUserCreateReviewUseCase
{
    public async Task<Guid> HandleAsync(UserCreateReviewCommand command, CancellationToken cancellationToken = default)
    {
        ProductReview review = new(
            userId: identityUserAccessor.GetUserId(),
            productId: command.ProductId,
            text: command.Text,
            createdAtUtc: dateTimeProvider.UtcNow
        );

        ValidationResult validationResult = ProductReviewValidator.Validate(review);
        ResponseValidationException.ThrowIf(validationResult.Failed, validationResult.Errors);

        await dbContext.ProductsReviews.AddAsync(review, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return review.Id;
    }
}