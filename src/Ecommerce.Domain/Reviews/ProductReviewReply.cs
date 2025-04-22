using Ecommerce.Domain.Users;

namespace Ecommerce.Domain.Reviews;

public class ProductReviewReply(
    Guid userId,
    Guid reviewId,
    string text,
    DateTime createdAtUtc
)
{
    public const string TableName = "ProductReviewReplies";
    public const int MaxTextLength = 50_000;

    public Guid Id { get; init; } = Guid.NewGuid();

    public Guid UserId { get; private set; } = userId;
    
    public Guid ReviewId { get; private set; } = reviewId;

    public ProductReview Review { get; private set; } = null!;

    public string Text { get; private set; } = text;

    public DateTime CreatedAtUtc { get; private set; } = createdAtUtc;
}