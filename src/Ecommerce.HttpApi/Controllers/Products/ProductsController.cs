using Ecommerce.Core.Products.GetById;
using Ecommerce.Core.Products.GetList;
using Ecommerce.Extensions.Requests;
using Ecommerce.Extensions.Types;
using Ecommerce.HttpApi.Contracts;
using Ecommerce.HttpApi.Contracts.Products;
using Ecommerce.HttpApi.Contracts.Products.GetList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.HttpApi.Controllers.Products;

[ApiController]
[Route("/api/v1/products")]
public class ProductsController(
    IGetProductByIdUseCase getProductByIdUseCase,
    IGetProductsListUseCase getProductsListUseCase
) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<EndpointResult<ProductResponse>>> GetById([FromRoute] Guid id,
        CancellationToken cancellationToken) =>
        new EndpointResult<ProductResponse>(
            new ProductResponse(await getProductByIdUseCase.HandleAsync(id, cancellationToken))
        );

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<EndpointResult<ProductsListResponse>>> GetList(
        [FromQuery] PaginationRequest request, CancellationToken cancellationToken)
    {
        PaginatedEnumerable<ProductDto> products =
            await getProductsListUseCase.HandleAsync(
                new PaginationQuery(request.PageSize, request.PageNumber),
                cancellationToken);

        ProductsListResponse response = new(products.Map(p => new ProductResponse(p)));

        return new EndpointResult<ProductsListResponse>(response);
    }
}