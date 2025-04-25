using Ecommerce.Persistence.Domain.Reviews;

namespace Ecommerce.Core.Users.Reviews;

public class ProductReviewDto(Guid userId, string text, DateTime createdAt)
{
    public ProductReviewDto(ProductReview review)
        : this(review.UserId, review.Text, review.CreatedAtUtc)
    {
    }

    public Guid UserId { get; private set; } = userId;

    public string Text { get; private set; } = text;

    public DateTime CreatedAt { get; private set; } = createdAt;
}