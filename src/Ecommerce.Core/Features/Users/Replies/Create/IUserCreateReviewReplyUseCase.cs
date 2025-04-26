namespace Ecommerce.Core.Features.Users.Replies.Create;

public interface IUserCreateReviewReplyUseCase
{
    public Task<Guid> HandleAsync(UserCreateReviewReplyCommand command, CancellationToken cancellationToken = default);
}