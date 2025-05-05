using Ecommerce.Persistence.Domain.Products.Enums;

namespace Ecommerce.Core.Features.Products.Create;

public record AdminCreateProductCommand(
    string Name,
    string Description,
    string Sku,
    ProductCategory Category,
    string Brand,
    string SellerName,
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