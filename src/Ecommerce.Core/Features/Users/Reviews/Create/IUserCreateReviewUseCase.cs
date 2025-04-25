namespace Ecommerce.Core.Features.Users.Reviews.Create;

public interface IUserCreateReviewUseCase
{
    public Task<Guid> HandleAsync(UserCreateReviewCommand command, CancellationToken cancellationToken = default);
}