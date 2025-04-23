using Ecommerce.Core.Exceptions.Products;
using Ecommerce.Domain.Products;
using Ecommerce.Extensions.Exceptions;
using Ecommerce.Extensions.Time;
using Ecommerce.Infrastructure.Database.Products;
using Ecommerce.Infrastructure.Repositories.Products;

namespace Ecommerce.Core.Admin.Products.Update;

public class AdminUpdateProductUseCase(
    IProductsRepository productsRepository,
    IProductsUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider
) : IAdminUpdateProductUseCase
{
    public async Task HandleAsync(AdminUpdateProductCommand command, CancellationToken cancellationToken = default)
    {
        DateTime utcNow = dateTimeProvider.UtcNow;

        Product product = await productsRepository.GetByIdAsync(command.ProductId, true, cancellationToken)
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

        ValidationResult validationResult = ProductValidator.Validate(product);
        ResponseValidationException.ThrowIf(validationResult.Failed, validationResult.Errors);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}