using Ecommerce.Core.Features.Products;
using Ecommerce.Core.Features.Products.GetById;
using Ecommerce.Core.Features.Products.GetList;
using Ecommerce.Extensions.Requests;
using Ecommerce.Extensions.Types;
using Ecommerce.HttpApi.Contracts;
using Ecommerce.HttpApi.Contracts.Products;
using Ecommerce.HttpApi.Contracts.Products.GetList;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.HttpApi.Endpoints;

public static class ProductsEndpoints
{
    public static RouteGroupBuilder MapProductsEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetList).AllowAnonymous().WithOpenApi();
        group.MapGet("/{id:guid}", GetById).AllowAnonymous().WithOpenApi();

        return group;
    }

    private static async Task<EndpointResult<ProductResponse>> GetById(
        Guid id,
        [FromServices] IGetProductByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        ProductDto product = await useCase.HandleAsync(id, cancellationToken);
        return new EndpointResult<ProductResponse>(new ProductResponse(product));
    }

    private static async Task<EndpointResult<ProductsListResponse>> GetList(
        [AsParameters] PaginationRequest request,
        [FromServices] IGetProductsListUseCase useCase,
        CancellationToken cancellationToken)
    {
        PaginatedEnumerable<ProductDto> products = await useCase.HandleAsync(request.ToPaginationQuery(), cancellationToken);

        ProductsListResponse response = new(products.Map(p => new ProductResponse(p)));

        return new EndpointResult<ProductsListResponse>(response);
    }
}