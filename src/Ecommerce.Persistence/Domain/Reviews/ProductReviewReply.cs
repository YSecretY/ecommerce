using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Persistence.Domain.Reviews;

public class ProductReviewReply(
    Guid userId,
    Guid reviewId,
    string text,
    DateTime createdAtUtc
)
{
    public const string TableName = "ProductReviewReplies";

    public Guid Id { get; init; } = Guid.NewGuid();

    public Guid UserId { get; private set; } = userId;

    public Guid ReviewId { get; private set; } = reviewId;

    public ProductReview Review { get; private set; } = null!;

    [MaxLength(ProductReviewReplyValidator.MaxTextLength)]
    public string Text { get; private set; } = text;

    public DateTime CreatedAtUtc { get; private set; } = createdAtUtc;

    public bool IsDeleted { get; private set; }

    public void Delete() =>
        IsDeleted = true;
}