using Ecommerce.Core.Exceptions.Products;
using Ecommerce.Infrastructure.Time;
using Ecommerce.Persistence.Database;
using Ecommerce.Persistence.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Core.Features.Admin.Products.Update;

public class AdminUpdateProductUseCase(
    ApplicationDbContext dbContext,
    IDateTimeProvider dateTimeProvider
) : IAdminUpdateProductUseCase
{
    public async Task HandleAsync(AdminUpdateProductCommand command, CancellationToken cancellationToken = default)
    {
        DateTime utcNow = dateTimeProvider.UtcNow;

        Product product = await dbContext.Products
                              .FirstOrDefaultAsync(p => p.Id == command.ProductId, cancellationToken)
                          ?? throw new ProductNotFoundException();

        product.Update(
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
            createdAtUtc: command.CreatedAtUtc,
            updatedAtUtc: utcNow,
            saleStartsAtUtc: command.SaleStartsAtUtc,
            saleEndsAtUtc: command.SaleEndsAtUtc
        );

        ProductValidator.Validate(product);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}