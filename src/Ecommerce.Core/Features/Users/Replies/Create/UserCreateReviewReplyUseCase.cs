using Ecommerce.Infrastructure.Auth.Abstractions;
using Ecommerce.Infrastructure.Time;
using Ecommerce.Persistence.Database;
using Ecommerce.Persistence.Domain.Reviews;

namespace Ecommerce.Core.Features.Users.Replies.Create;

public class UserCreateReviewReplyUseCase(
    ApplicationDbContext dbContext,
    IIdentityUserAccessor identityUserAccessor,
    IDateTimeProvider dateTimeProvider
) : IUserCreateReviewReplyUseCase
{
    public async Task<Guid> HandleAsync(UserCreateReviewReplyCommand command, CancellationToken cancellationToken = default)
    {
        Guid userId = identityUserAccessor.GetUserId();

        ProductReviewReply reply = ProductReviewReplyValidator.CreateValid(
            userId,
            command.ReviewId,
            command.Text,
            dateTimeProvider.UtcNow
        );

        await dbContext.AddAsync(reply, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return reply.Id;
    }
}