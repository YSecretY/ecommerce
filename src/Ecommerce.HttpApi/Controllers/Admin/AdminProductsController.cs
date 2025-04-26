using Ecommerce.Core.Features.Admin.Products.Create;
using Ecommerce.Core.Features.Admin.Products.DeleteById;
using Ecommerce.Core.Features.Admin.Products.DeleteList;
using Ecommerce.Core.Features.Admin.Products.Update;
using Ecommerce.Extensions.Requests;
using Ecommerce.HttpApi.Contracts.Admin.Products;
using Ecommerce.Persistence.Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.HttpApi.Controllers.Admin;

[ApiController]
[Authorize(Roles = nameof(UserRole.Admin))]
[Route("/api/v1/admin/products")]
public class AdminProductsController(
    IAdminCreateProductUseCase createProductUseCase,
    IAdminUpdateProductUseCase updateProductUseCase,
    IAdminDeleteProductByIdUseCase deleteProductByIdUseCase,
    IAdminDeleteProductsListUseCase deleteProductsListUseCase
)
{
    [HttpPost]
    public async Task<ActionResult<EndpointResult<Guid>>> CreateProduct([FromBody] AdminCreateProductRequest request,
        CancellationToken cancellationToken)
    {
        AdminCreateProductCommand command = new(
            Name: request.Name,
            Description: request.Description,
            Sku: request.Sku,
            Brand: request.Brand,
            Price: request.Price,
            SalePrice: request.SalePrice,
            MainImageUrl: request.MainImageUrl,
            ImageGalleryUrls: request.ImageGalleryUrls,
            CurrencyCode: request.CurrencyCode,
            CountryCode: request.CountryCode,
            TotalCount: request.TotalCount,
            IsInStock: request.IsInStock,
            SaleStartsAtUtc: request.SaleStartsAtUtc,
            SaleEndsAtUtc: request.SaleEndsAtUtc
        );

        return new EndpointResult<Guid>(await createProductUseCase.HandleAsync(command, cancellationToken));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct([FromBody] AdminUpdateProductRequest request,
        CancellationToken cancellationToken)
    {
        AdminUpdateProductCommand command = new(
            ProductId: request.ProductId,
            Name: request.Name,
            Description: request.Description,
            Sku: request.Sku,
            Brand: request.Brand,
            Price: request.Price,
            SalePrice: request.SalePrice,
            MainImageUrl: request.MainImageUrl,
            ImageGalleryUrls: request.ImageGalleryUrls,
            CurrencyCode: request.CurrencyCode,
            CountryCode: request.CountryCode,
            TotalCount: request.TotalCount,
            IsInStock: request.IsInStock,
            CreatedAtUtc: request.CreatedAtUtc,
            SaleStartsAtUtc: request.SaleStartsAtUtc,
            SaleEndsAtUtc: request.SaleEndsAtUtc
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

    [HttpDelete]
    public async Task<IActionResult> DeleteProductsList([FromBody] AdminDeleteProductsListRequest request,
        CancellationToken cancellationToken)
    {
        await deleteProductsListUseCase.HandleAsync(request.ProductsIds, cancellationToken);

        return new OkResult();
    }
}