namespace Ecommerce.Core.Admin.Products.Update;

public class AdminUpdateProductCommand(
    Guid productId,
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
    DateTime createdAtUtc,
    DateTime updatedAtUtc,
    DateTime? saleStartsAtUtc,
    DateTime? saleEndsAtUtc)
{
    public Guid ProductId { get; private set; } = productId;

    public string Name { get; private set; } = name;

    public string Description { get; private set; } = description;

    public string Sku { get; private set; } = sku;

    public string Brand { get; private set; } = brand;

    public decimal Price { get; private set; } = price;

    public decimal? SalePrice { get; private set; } = salePrice;

    public string MainImageUrl { get; private set; } = mainImageUrl;

    public List<string>? ImageGalleryUrls { get; private set; } = imageGalleryUrls;

    public string CurrencyCode { get; private set; } = currencyCode;

    public string CountryCode { get; private set; } = countryCode;

    public long TotalCount { get; private set; } = totalCount;

    public DateTime CreatedAtUtc { get; private set; } = createdAtUtc;

    public DateTime UpdatedAtUtc { get; private set; } = updatedAtUtc;

    public bool IsInStock { get; private set; } = isInStock;

    public DateTime? SaleStartsAtUtc { get; private set; } = saleStartsAtUtc;

    public DateTime? SaleEndsAtUtc { get; private set; } = saleEndsAtUtc;
}