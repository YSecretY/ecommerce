using System.ComponentModel.DataAnnotations;
using Ecommerce.Persistence.Domain.Products;
using Ecommerce.Persistence.Domain.Users;

namespace Ecommerce.Persistence.Domain.Reviews;

public class ProductReview(
    Guid userId,
    Guid productId,
    string text,
    DateTime createdAtUtc
) : ISoftDeletable
{
    public const string TableName = "ProductsReviews";

    public Guid Id { get; init; } = Guid.NewGuid();

    public Guid UserId { get; private set; } = userId;

    public User User { get; private set; } = null!;

    public Guid ProductId { get; private set; } = productId;

    public Product Product { get; private set; } = null!;

    [MaxLength(ProductReviewValidator.MaxTextLength)]
    public string Text { get; private set; } = text;

    public ICollection<ProductReviewReply> Replies { get; private set; } = null!;

    public DateTime CreatedAtUtc { get; private set; } = createdAtUtc;

    public bool IsDeleted { get; set; }

    public void SoftDelete()
    {
        IsDeleted = true;

        foreach (ProductReviewReply reply in Replies)
            reply.SoftDelete();
    }
}