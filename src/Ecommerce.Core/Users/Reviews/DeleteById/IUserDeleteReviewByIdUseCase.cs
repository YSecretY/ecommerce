namespace Ecommerce.Core.Users.Reviews.DeleteById;

public interface IUserDeleteReviewByIdUseCase
{
    public Task HandleAsync(Guid id, CancellationToken cancellationToken = default);
}