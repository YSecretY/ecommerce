using Ecommerce.Extensions.Exceptions;
using Ecommerce.Extensions.Time;
using Ecommerce.Persistence.Database;
using Ecommerce.Persistence.Domain.Products;

namespace Ecommerce.Core.Admin.Products.Create;

public class AdminCreateProductUseCase(
    ProductsDbContext dbContext,
    IDateTimeProvider dateTimeProvider
) : IAdminCreateProductUseCase
{
    public async Task<Guid> HandleAsync(AdminCreateProductCommand command, CancellationToken cancellationToken = default)
    {
        DateTime utcNow = dateTimeProvider.UtcNow;

        Product product = new(
            name: command.Name,
            description: command.Description,
            sku: command.Sku,
            brand: command.Brand,
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

        ValidationResult validationResult = ProductValidator.Validate(product);
        ResponseValidationException.ThrowIf(validationResult.Failed, validationResult.Errors);

        await dbContext.Products.AddAsync(product, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}