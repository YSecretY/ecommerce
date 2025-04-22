using Ecommerce.Domain.Products;
using Ecommerce.Extensions.Exceptions;
using Ecommerce.Infrastructure.Database.Products;
using Ecommerce.Infrastructure.Repositories.Products;

namespace Ecommerce.Core.Admin.Products.Create;

public class AdminCreateProductUseCase(
    IProductsRepository productsRepository,
    IProductsUnitOfWork unitOfWork
) : IAdminCreateProductUseCase
{
    public async Task<Guid> HandleAsync(AdminCreateProductCommand command, CancellationToken cancellationToken = default)
    {
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
            createdAtUtc: command.CreatedAtUtc,
            updatedAtUtc: command.UpdatedAtUtc,
            saleStartsAtUtc: command.SaleStartsAtUtc,
            saleEndsAtUtc: command.SaleEndsAtUtc
        );

        ValidationResult validationResult = ProductValidator.Validate(product);
        ResponseValidationException.ThrowIf(validationResult.Failed, validationResult.Errors);

        await productsRepository.AddAsync(product, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}