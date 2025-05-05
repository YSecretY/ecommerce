namespace Ecommerce.Core.Features.Reviews.Create;

public interface IUserCreateReviewUseCase
{
    public Task<Guid> HandleAsync(UserCreateReviewCommand command, CancellationToken cancellationToken = default);
}