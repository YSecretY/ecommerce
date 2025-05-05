using Ecommerce.Infrastructure.Time;
using Ecommerce.Persistence.Database;
using Ecommerce.Persistence.Domain.Products;

namespace Ecommerce.Core.Features.Products.Create;

public class AdminCreateProductUseCase(
    ApplicationDbContext dbContext,
    IDateTimeProvider dateTimeProvider
) : IAdminCreateProductUseCase
{
    public async Task<Guid> HandleAsync(AdminCreateProductCommand command, CancellationToken cancellationToken = default)
    {
        DateTime utcNow = dateTimeProvider.UtcNow;

        Product product = ProductValidator.CreateValid(
            name: command.Name,
            description: command.Description,
            sku: command.Sku,
            category: command.Category,
            brand: command.Brand,
            sellerName: command.SellerName,
            price: command.Price,
            salePrice: command.SalePrice,
            mainImageUrl: command.MainImageUrl,
            imageGalleryUrls: command.ImageGalleryUrls,
            currencyCode: command.CurrencyCode,
            countryCode: command.CountryCode,
            totalCount: command.TotalCount,
            isInStock: command.IsInStock,
            createdAtUtc: utcNow,
            updatedAtUtc: utcNow,
            saleStartsAtUtc: command.SaleStartsAtUtc,
            saleEndsAtUtc: command.SaleEndsAtUtc
        );

        await dbContext.Products.AddAsync(product, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}