using System.Text.Json.Serialization;

namespace Ecommerce.HttpApi.Contracts.Admin.Products;

public class AdminCreateProductRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("sku")]
    public string Sku { get; set; } = string.Empty;

    [JsonPropertyName("brand")]
    public string Brand { get; set; } = string.Empty;

    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("salePrice")]
    public decimal? SalePrice { get; set; }

    [JsonPropertyName("mainImageUrl")]
    public string MainImageUrl { get; set; } = string.Empty;

    [JsonPropertyName("imageGalleryUrls")]
    public List<string>? ImageGalleryUrls { get; set; }

    [JsonPropertyName("currencyCode")]
    public string CurrencyCode { get; set; } = string.Empty;

    [JsonPropertyName("countryCode")]
    public string CountryCode { get; set; } = string.Empty;

    [JsonPropertyName("totalCount")]
    public long TotalCount { get; set; }

    [JsonPropertyName("isInStock")]
    public bool IsInStock { get; set; }

    [JsonPropertyName("saleStartsAtUtc")]
    public DateTime? SaleStartsAtUtc { get; set; }

    [JsonPropertyName("saleEndsAtUtc")]
    public DateTime? SaleEndsAtUtc { get; set; }
}