using Ecommerce.Core.Auth.Shared;
using Ecommerce.Domain.Reviews;
using Ecommerce.Extensions.Exceptions;
using Ecommerce.Extensions.Time;
using Ecommerce.Infrastructure.Database.Products;
using Ecommerce.Infrastructure.Repositories.Reviews;

namespace Ecommerce.Core.Users.Reviews.Create;

public class UserCreateReviewUseCase(
    IProductsReviewsRepository reviewsRepository,
    IProductsUnitOfWork unitOfWork,
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

        await reviewsRepository.AddAsync(review, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return review.Id;
    }
}