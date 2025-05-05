using System.Text.Json.Serialization;
using Ecommerce.Core.Features.Products.Create;
using Ecommerce.Extensions.Types;
using Ecommerce.Persistence.Domain.Products.Enums;

namespace Ecommerce.HttpApi.Contracts.Admin.Products;

public class AdminCreateProductRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("sku")]
    public string Sku { get; set; } = string.Empty;

    [JsonPropertyName("category")]
    public string Category { get; set; } = string.Empty;

    [JsonPropertyName("brand")]
    public string Brand { get; set; } = string.Empty;

    [JsonPropertyName("sellerName")]
    public string SellerName { get; set; } = string.Empty;

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

    public AdminCreateProductCommand ToCommand() => new(
        Name: Name,
        Description: Description,
        Sku: Sku,
        Category: Category.ToEnum<ProductCategory>(),
        Brand: Brand,
        SellerName: SellerName,
        Price: Price,
        SalePrice: SalePrice,
        MainImageUrl: MainImageUrl,
        ImageGalleryUrls: ImageGalleryUrls,
        CurrencyCode: CurrencyCode,
        CountryCode: CountryCode,
        TotalCount: TotalCount,
        IsInStock: IsInStock,
        SaleStartsAtUtc: SaleStartsAtUtc,
        SaleEndsAtUtc: SaleEndsAtUtc
    );
}