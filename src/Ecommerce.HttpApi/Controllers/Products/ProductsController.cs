using Ecommerce.Core.Products.GetById;
using Ecommerce.Extensions.Requests;
using Ecommerce.HttpApi.Contracts.Products.GetById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.HttpApi.Controllers.Products;

[ApiController]
[Route("/api/v1/products")]
public class ProductsController(
    IGetProductByIdUseCase getProductByIdUseCase
) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<EndpointResult<ProductResponse>>> GetById([FromRoute] Guid id,
        CancellationToken cancellationToken) =>
        new EndpointResult<ProductResponse>(
            new ProductResponse(await getProductByIdUseCase.HandleAsync(id, cancellationToken))
        );
}