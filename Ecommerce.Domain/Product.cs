using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Domain;

public class Product(
    string name,
    string description,
    string sku,
    string brand,
    decimal price,
    decimal? salePrice,
    string mainImageUrl,
    List<string>? imageGalleryUrls,
    string currencyCode,
    string countryCode,
    long totalCount,
    bool isInStock,
    bool isOnSale,
    DateTime createdAtUtc,
    DateTime updatedAtUtc
)
{
    public const int MaxNameLength = 512;
    public const int MaxDescriptionLength = 2056;
    public const int MaxSkuLength = 256;
    public const int MaxBrandLength = 256;
    public const int MaxImageUrlLength = 2056;
    public const int MaxCurrencyCodeLength = 3;
    public const int MaxCountryCodeLength = 3;

    public Guid Id { get; init; } = Guid.NewGuid();

    [MaxLength(MaxNameLength)]
    public string Name { get; private set; } = name;

    [MaxLength(MaxDescriptionLength)]
    public string Description { get; private set; } = description;

    [MaxLength(MaxSkuLength)]
    public string Sku { get; private set; } = sku;

    [MaxLength(MaxBrandLength)]
    public string Brand { get; private set; } = brand;

    public decimal Price { get; private set; } = price;

    public decimal? SalePrice { get; private set; } = salePrice;

    [MaxLength(MaxImageUrlLength)]
    public string MainImageUrl { get; private set; } = mainImageUrl;

    public List<string>? ImageGalleryUrls { get; private set; } = imageGalleryUrls;

    [MaxLength(MaxCurrencyCodeLength)]
    public string CurrencyCode { get; private set; } = currencyCode;

    [MaxLength(MaxCountryCodeLength)]
    public string CountryCode { get; private set; } = countryCode;

    public long TotalCount { get; private set; } = totalCount;

    public DateTime CreatedAtUtc { get; private set; } = createdAtUtc;

    public DateTime UpdatedAtUtc { get; private set; } = updatedAtUtc;

    public bool IsInStock { get; private set; } = isInStock;

    public bool IsOnSale { get; private set; } = isOnSale;
}