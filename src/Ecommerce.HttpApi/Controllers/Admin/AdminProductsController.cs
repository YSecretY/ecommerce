using Ecommerce.Core.Admin.Products.Create;
using Ecommerce.Core.Admin.Products.DeleteById;
using Ecommerce.Core.Admin.Products.Update;
using Ecommerce.Domain.Users;
using Ecommerce.Extensions.Requests;
using Ecommerce.HttpApi.Contracts.Admin.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.HttpApi.Controllers.Admin;

[ApiController]
[Authorize(Roles = nameof(UserRole.Admin))]
[Route("/api/v1/admin/products")]
public class AdminProductsController(
    IAdminCreateProductUseCase createProductUseCase,
    IAdminUpdateProductUseCase updateProductUseCase,
    IAdminDeleteProductByIdUseCase deleteProductByIdUseCase
)
{
    [HttpPost]
    public async Task<ActionResult<EndpointResult<Guid>>> CreateProduct([FromBody] AdminCreateProductRequest request,
        CancellationToken cancellationToken)
    {
        AdminCreateProductCommand command = new(
            name: request.Name,
            description: request.Description,
            sku: request.Sku,
            brand: request.Brand,
            price: request.Price,
            salePrice: request.SalePrice,
            mainImageUrl: request.MainImageUrl,
            imageGalleryUrls: request.ImageGalleryUrls,
            currencyCode: request.CurrencyCode,
            countryCode: request.CountryCode,
            totalCount: request.TotalCount,
            isInStock: request.IsInStock,
            saleStartsAtUtc: request.SaleStartsAtUtc,
            saleEndsAtUtc: request.SaleEndsAtUtc
        );

        return new EndpointResult<Guid>(await createProductUseCase.HandleAsync(command, cancellationToken));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct([FromBody] AdminUpdateProductRequest request,
        CancellationToken cancellationToken)
    {
        AdminUpdateProductCommand command = new(
            productId: request.ProductId,
            name: request.Name,
            description: request.Description,
            sku: request.Sku,
            brand: request.Brand,
            price: request.Price,
            salePrice: request.SalePrice,
            mainImageUrl: request.MainImageUrl,
            imageGalleryUrls: request.ImageGalleryUrls,
            currencyCode: request.CurrencyCode,
            countryCode: request.CountryCode,
            totalCount: request.TotalCount,
            isInStock: request.IsInStock,
            createdAtUtc: request.CreatedAtUtc,
            saleStartsAtUtc: request.SaleStartsAtUtc,
            saleEndsAtUtc: request.SaleEndsAtUtc
        );

        await updateProductUseCase.HandleAsync(command, cancellationToken);

        return new OkResult();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await deleteProductByIdUseCase.HandleAsync(id, cancellationToken);

        return new OkResult();
    }
}