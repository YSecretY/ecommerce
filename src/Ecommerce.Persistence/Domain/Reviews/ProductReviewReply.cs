using System.ComponentModel.DataAnnotations;
using Ecommerce.Persistence.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Persistence.Domain.Reviews;

public class ProductReviewReply(
    Guid userId,
    Guid reviewId,
    string text,
    DateTime createdAtUtc
) : ISoftDeletable
{
    private const string TableName = "ProductReviewReplies";

    public Guid Id { get; init; } = Guid.NewGuid();

    public Guid UserId { get; private set; } = userId;

    public User User { get; private set; } = null!;

    public Guid ReviewId { get; private set; } = reviewId;

    public ProductReview Review { get; private set; } = null!;

    [MaxLength(ProductReviewReplyValidator.MaxTextLength)]
    public string Text { get; private set; } = text;

    public DateTime CreatedAtUtc { get; private set; } = createdAtUtc;

    public bool IsDeleted { get; set; }

    public void SoftDelete() =>
        IsDeleted = true;

    public static void Builder(EntityTypeBuilder<ProductReviewReply> reply)
    {
        reply.ToTable(ProductReviewReply.TableName);

        reply.HasKey(r => r.Id);

        reply.HasIndex(r => r.ReviewId);

        reply.Property(r => r.Text)
            .IsRequired()
            .HasMaxLength(ProductReviewValidator.MaxTextLength);

        reply.HasOne(r => r.Review)
            .WithMany(r => r.Replies)
            .HasForeignKey(r => r.ReviewId)
            .OnDelete(DeleteBehavior.Cascade);

        reply.Property(r => r.CreatedAtUtc)
            .IsRequired();

        reply.HasQueryFilter(r => !r.IsDeleted);
    }
}