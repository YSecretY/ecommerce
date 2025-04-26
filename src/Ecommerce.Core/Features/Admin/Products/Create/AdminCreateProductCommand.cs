namespace Ecommerce.Core.Features.Admin.Products.Create;

public record AdminCreateProductCommand(
    string Name,
    string Description,
    string Sku,
    string Brand,
    decimal Price,
    decimal? SalePrice,
    string MainImageUrl,
    List<string>? ImageGalleryUrls,
    string CurrencyCode,
    string CountryCode,
    long TotalCount,
    bool IsInStock,
    DateTime? SaleStartsAtUtc,
    DateTime? SaleEndsAtUtc
);