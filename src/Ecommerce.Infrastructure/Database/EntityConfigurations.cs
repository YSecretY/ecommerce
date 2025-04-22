using Ecommerce.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Database;

public static class EntityConfigurations
{
    public static void ApplyAllProductsDatabaseEntityTypeConfigurations(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyProductConfigurations();
        modelBuilder.ApplyReviewsConfigurations();
        modelBuilder.ApplyReviewRepliesConfigurations();
    }

    public static void ApplyAllUsersDatabaseEntityTypeConfigurations(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyUserConfigurations();
    }

    private static void ApplyProductConfigurations(this ModelBuilder modelBuilder)
    {
        EntityTypeBuilder<Product> productBuilder = modelBuilder.Entity<Product>();

        productBuilder.ToTable(Product.TableName);

        productBuilder.HasKey(p => p.Id);

        productBuilder.HasIndex(p => p.Sku);

        productBuilder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(Product.MaxNameLength);

        productBuilder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(Product.MaxDescriptionLength);

        productBuilder.Property(p => p.Sku)
            .IsRequired()
            .HasMaxLength(Product.MaxSkuLength);

        productBuilder.Property(p => p.Brand)
            .HasMaxLength(Product.MaxBrandLength);

        productBuilder.Property(p => p.MainImageUrl)
            .IsRequired()
            .HasMaxLength(Product.MaxImageUrlLength);

        productBuilder.Property(p => p.CurrencyCode)
            .IsRequired()
            .HasMaxLength(Product.MaxCurrencyCodeLength);

        productBuilder.Property(p => p.CountryCode)
            .IsRequired()
            .HasMaxLength(Product.MaxCountryCodeLength);

        productBuilder.Property(p => p.TotalCount)
            .IsRequired();

        productBuilder.Property(p => p.IsInStock)
            .IsRequired();

        productBuilder.Property(p => p.CreatedAtUtc)
            .IsRequired();

        productBuilder.Property(p => p.UpdatedAtUtc)
            .IsRequired();

        productBuilder.Ignore(p => p.IsOnSale);

        productBuilder.Ignore(p => p.DisplayPrice);
    }

    private static void ApplyUserConfigurations(this ModelBuilder modelBuilder)
    {
        EntityTypeBuilder<User> userBuilder = modelBuilder.Entity<User>();

        userBuilder.ToTable(User.TableName);

        userBuilder.HasKey(u => u.Id);

        userBuilder.HasIndex(u => u.Email).IsUnique();

        userBuilder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(User.MaxEmailLength);

        userBuilder.Property(u => u.PasswordHash)
            .IsRequired()
            .HasMaxLength(User.MaxPasswordHashLength);

        userBuilder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(User.MaxNameLength);

        userBuilder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(User.MaxNameLength);

        userBuilder.Property(u => u.IsEmailConfirmed)
            .IsRequired();

        userBuilder.Property(u => u.Role)
            .IsRequired();

        userBuilder.Property(u => u.CreatedAtUtc)
            .IsRequired();
    }

    private static void ApplyReviewsConfigurations(this ModelBuilder modelBuilder)
    {
        EntityTypeBuilder<ProductReview> review = modelBuilder.Entity<ProductReview>();

        review.ToTable(ProductReview.TableName);

        review.HasKey(r => r.Id);

        review.HasIndex(r => r.ProductId);

        review.Property(r => r.Text)
            .IsRequired()
            .HasMaxLength(ProductReview.MaxTextLength);

        review.HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        review.HasOne(r => r.Product)
            .WithMany(p => p.Reviews)
            .HasForeignKey(r => r.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        review.Property(r => r.CreatedAtUtc)
            .IsRequired();
    }

    private static void ApplyReviewRepliesConfigurations(this ModelBuilder modelBuilder)
    {
        EntityTypeBuilder<ProductReviewReply> reviewReply = modelBuilder.Entity<ProductReviewReply>();

        reviewReply.ToTable(ProductReviewReply.TableName);

        reviewReply.HasKey(r => r.Id);

        reviewReply.HasIndex(r => r.ReviewId);

        reviewReply.Property(r => r.Text)
            .IsRequired()
            .HasMaxLength(ProductReview.MaxTextLength);

        reviewReply.HasOne(r => r.User)
            .WithMany()
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        reviewReply.HasOne(r => r.Review)
            .WithMany(r => r.Replies)
            .HasForeignKey(r => r.ReviewId)
            .OnDelete(DeleteBehavior.Cascade);

        reviewReply.Property(r => r.CreatedAtUtc)
            .IsRequired();
    }
}