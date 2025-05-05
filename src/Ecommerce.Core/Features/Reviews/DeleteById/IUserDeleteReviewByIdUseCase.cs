namespace Ecommerce.Core.Features.Reviews.DeleteById;

public interface IUserDeleteReviewByIdUseCase
{
    public Task HandleAsync(Guid id, CancellationToken cancellationToken = default);
}