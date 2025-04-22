namespace Ecommerce.Domain;

public class ProductReview(
    Guid userId,
    Guid productId,
    string text,
    DateTime createdAtUtc
)
{
    public const string TableName = "ProductsReviews";
    public const int MaxTextLength = 50_000;

    public Guid Id { get; init; } = Guid.NewGuid();

    public Guid UserId { get; private set; } = userId;

    public User User { get; private set; } = null!;

    public Guid ProductId { get; private set; } = productId;

    public Product Product { get; private set; } = null!;

    public string Text { get; private set; } = text;

    public ICollection<ProductReviewReply> Replies { get; private set; } = null!;

    public DateTime CreatedAtUtc { get; private set; } = createdAtUtc;
}