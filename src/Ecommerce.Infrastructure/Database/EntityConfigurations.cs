using Ecommerce.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Database;

public static class EntityConfigurations
{
    public static void ApplyAllProductsDatabaseEntityTypeConfigurations(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyProductConfigurations();
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
}