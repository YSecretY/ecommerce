using System.Text.Json.Serialization;
using Ecommerce.Core.Products;

namespace Ecommerce.HttpApi.Contracts.Products;

public class ProductResponse(
    Guid id,
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
    DateTime? saleEndsAtUtc
)
{
    public ProductResponse(ProductDto dto)
        : this(
            id: dto.Id,
            name: dto.Name,
            description: dto.Description,
            sku: dto.Sku,
            brand: dto.Brand,
            price: dto.Price,
            salePrice: dto.SalePrice,
            mainImageUrl: dto.MainImageUrl,
            imageGalleryUrls: dto.ImageGalleryUrls,
            currencyCode: dto.CurrencyCode,
            countryCode: dto.CountryCode,
            totalCount: dto.TotalCount,
            isInStock: dto.IsInStock,
            createdAtUtc: dto.CreatedAtUtc,
            updatedAtUtc: dto.UpdatedAtUtc,
            saleStartsAtUtc: dto.SaleStartsAtUtc,
            saleEndsAtUtc: dto.SaleEndsAtUtc
        )
    {
    }

    [JsonPropertyName("id")]
    public Guid Id { get; init; } = id;

    [JsonPropertyName("name")]
    public string Name { get; private set; } = name;

    [JsonPropertyName("description")]
    public string Description { get; private set; } = description;

    [JsonPropertyName("sku")]
    public string Sku { get; private set; } = sku;

    [JsonPropertyName("brand")]
    public string Brand { get; private set; } = brand;

    [JsonPropertyName("price")]
    public decimal Price { get; private set; } = price;

    [JsonPropertyName("salePrice")]
    public decimal? SalePrice { get; private set; } = salePrice;

    [JsonPropertyName("mainImageUrl")]
    public string MainImageUrl { get; private set; } = mainImageUrl;

    [JsonPropertyName("imageGalleryUrls")]
    public List<string>? ImageGalleryUrls { get; private set; } = imageGalleryUrls;

    [JsonPropertyName("currencyCode")]
    public string CurrencyCode { get; private set; } = currencyCode;

    [JsonPropertyName("countryCode")]
    public string CountryCode { get; private set; } = countryCode;

    [JsonPropertyName("totalCount")]
    public long TotalCount { get; private set; } = totalCount;

    [JsonPropertyName("createdAtUtc")]
    public DateTime CreatedAtUtc { get; private set; } = createdAtUtc;

    [JsonPropertyName("updatedAtUtc")]
    public DateTime UpdatedAtUtc { get; private set; } = updatedAtUtc;

    [JsonPropertyName("isInStock")]
    public bool IsInStock { get; private set; } = isInStock;

    [JsonPropertyName("saleStartsAtUtc")]
    public DateTime? SaleStartsAtUtc { get; private set; } = saleStartsAtUtc;

    [JsonPropertyName("saleEndsAtUtc")]
    public DateTime? SaleEndsAtUtc { get; private set; } = saleEndsAtUtc;
}