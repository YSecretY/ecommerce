namespace Ecommerce.Core.Features.Admin.Products.Update;

public record AdminUpdateProductCommand(
    Guid ProductId,
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
    DateTime CreatedAtUtc,
    DateTime? SaleStartsAtUtc,
    DateTime? SaleEndsAtUtc
);