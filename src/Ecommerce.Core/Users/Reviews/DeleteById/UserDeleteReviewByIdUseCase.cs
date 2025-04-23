using Ecommerce.Core.Auth.Shared;
using Ecommerce.Core.Exceptions.Reviews;
using Ecommerce.Domain.Reviews;
using Ecommerce.Extensions.Exceptions;
using Ecommerce.Infrastructure.Database.Products;
using Ecommerce.Infrastructure.Repositories.Reviews;

namespace Ecommerce.Core.Users.Reviews.DeleteById;

public class UserDeleteReviewByIdUseCase(
    IIdentityUserAccessor identityUserAccessor,
    IProductsReviewsRepository reviewsRepository,
    IProductsUnitOfWork unitOfWork
) : IUserDeleteReviewByIdUseCase
{
    public async Task HandleAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Guid userId = identityUserAccessor.GetUserId();

        ProductReview review = await reviewsRepository.GetByIdAsync(id, cancellationToken: cancellationToken)
                               ?? throw new ProductReviewNotFoundException();

        if (review.UserId != userId)
            throw new UnauthorizedException();

        reviewsRepository.Remove(review);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}