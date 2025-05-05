namespace Ecommerce.Core.Features.Replies.Create;

public record UserCreateReviewReplyCommand(
    Guid ReviewId,
    string Text
);