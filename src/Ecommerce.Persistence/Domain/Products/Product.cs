using System.ComponentModel.DataAnnotations;
using Ecommerce.Persistence.Domain.Products.Enums;
using Ecommerce.Persistence.Domain.Reviews;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Persistence.Domain.Products;

public class Product(
    string name,
    string description,
    string sku,
    ProductCategory category,
    string brand,
    string sellerName,
    decimal price,
    decimal? salePrice,
    string mainImageUrl,
    List<string>? imageGalleryUrls,
    string currencyCode,
    string countryCode,
    long totalCount,
    bool isInStock,
    DateTime createdAtUtc,
    DateTime updatedAtUtc,
    DateTime? saleStartsAtUtc,
    DateTime? saleEndsAtUtc
) : ISoftDeletable
{
    private const string TableName = "Products";

    public Guid Id { get; init; } = Guid.NewGuid();

    [MaxLength(ProductValidator.MaxNameLength)]
    public string Name { get; private set; } = name;

    [MaxLength(ProductValidator.MaxDescriptionLength)]
    public string Description { get; private set; } = description;

    [MaxLength(ProductValidator.MaxSkuLength)]
    public string Sku { get; private set; } = sku;

    public ProductCategory Category { get; private set; } = category;

    [MaxLength(ProductValidator.MaxBrandLength)]
    public string Brand { get; private set; } = brand;

    [MaxLength(ProductValidator.MaxSellerNameLength)]
    public string SellerName { get; private set; } = sellerName;

    public decimal Price { get; private set; } = price;

    public decimal? SalePrice { get; private set; } = salePrice;

    [MaxLength(ProductValidator.MaxImageUrlLength)]
    public string MainImageUrl { get; private set; } = mainImageUrl;

    public List<string>? ImageGalleryUrls { get; private set; } = imageGalleryUrls;

    [MaxLength(ProductValidator.MaxCurrencyCodeLength)]
    public string CurrencyCode { get; private set; } = currencyCode;

    [MaxLength(ProductValidator.MaxCountryCodeLength)]
    public string CountryCode { get; private set; } = countryCode;

    public long TotalCount { get; private set; } = totalCount;

    public DateTime CreatedAtUtc { get; private set; } = createdAtUtc;

    public DateTime UpdatedAtUtc { get; private set; } = updatedAtUtc;

    public bool IsInStock { get; private set; } = isInStock;

    public DateTime? SaleStartsAtUtc { get; private set; } = saleStartsAtUtc;

    public DateTime? SaleEndsAtUtc { get; private set; } = saleEndsAtUtc;

    public bool IsDeleted { get; set; }

    public void Buy(int quantity) =>
        TotalCount -= quantity;

    public void SoftDelete()
    {
        IsDeleted = true;

        foreach (ProductReview review in Reviews)
            review.SoftDelete();
    }

    public ICollection<ProductReview> Reviews { get; private set; } = null!;

    public bool IsOnSale(DateTime utcNow) =>
        SalePrice.HasValue &&
        SaleStartsAtUtc <= utcNow &&
        SaleEndsAtUtc >= utcNow;

    public decimal DisplayPrice(DateTime utcNow) =>
        IsOnSale(utcNow) ? SalePrice!.Value : Price;

    public void Update(
        string name,
        string description,
        string sku,
        ProductCategory category,
        string brand,
        decimal price,
        decimal? salePrice,
        string mainImageUrl,
        List<string>? imageGalleryUrls,
        string currencyCode,
        string countryCode,
        long totalCount,
        bool isInStock,
        DateTime createdAtUtc,
        DateTime updatedAtUtc,
        DateTime? saleStartsAtUtc,
        DateTime? saleEndsAtUtc
    )
    {
        Name = name;
        Description = description;
        Sku = sku;
        Category = category;
        Brand = brand;
        Price = price;
        SalePrice = salePrice;
        MainImageUrl = mainImageUrl;
        ImageGalleryUrls = imageGalleryUrls;
        CurrencyCode = currencyCode;
        CountryCode = countryCode;
        TotalCount = totalCount;
        IsInStock = isInStock;
        CreatedAtUtc = createdAtUtc;
        UpdatedAtUtc = updatedAtUtc;
        SaleStartsAtUtc = saleStartsAtUtc;
        SaleEndsAtUtc = saleEndsAtUtc;
    }

    public static void Builder(EntityTypeBuilder<Product> product)
    {
        product.ToTable(TableName);

        product.HasKey(p => p.Id);

        product.HasIndex(p => p.Sku);

        product.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(ProductValidator.MaxNameLength);

        product.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(ProductValidator.MaxDescriptionLength);

        product.Property(p => p.Sku)
            .IsRequired()
            .HasMaxLength(ProductValidator.MaxSkuLength);

        product.Property(p => p.Brand)
            .HasMaxLength(ProductValidator.MaxBrandLength);

        product.Property(p => p.MainImageUrl)
            .IsRequired()
            .HasMaxLength(ProductValidator.MaxImageUrlLength);

        product.Property(p => p.CurrencyCode)
            .IsRequired()
            .HasMaxLength(ProductValidator.MaxCurrencyCodeLength);

        product.Property(p => p.CountryCode)
            .IsRequired()
            .HasMaxLength(ProductValidator.MaxCountryCodeLength);

        product.Property(p => p.TotalCount)
            .IsRequired();

        product.Property(p => p.IsInStock)
            .IsRequired();

        product.Property(p => p.CreatedAtUtc)
            .IsRequired();

        product.Property(p => p.UpdatedAtUtc)
            .IsRequired();

        product.HasQueryFilter(p => !p.IsDeleted);
    }
}