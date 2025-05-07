using Ecommerce.Core.Abstractions.Models.Products;
using Ecommerce.Core.Features.Products.Analytics.GetMostSoldProducts;
using Ecommerce.Core.Features.Products.Analytics.GetProductSales;
using Ecommerce.Core.Features.Products.GetById;
using Ecommerce.Core.Features.Products.GetList;
using Ecommerce.Extensions.Requests;
using Ecommerce.Extensions.Types;
using Ecommerce.HttpApi.Contracts;
using Ecommerce.HttpApi.Contracts.Products;
using Ecommerce.HttpApi.Contracts.Products.GetList;
using Ecommerce.Persistence.Domain.Users;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.HttpApi.Endpoints;

public static class ProductsEndpoints
{
    public static RouteGroupBuilder MapProductsEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetList).AllowAnonymous().WithOpenApi();
        group.MapGet("/{id:guid}", GetById).AllowAnonymous().WithOpenApi();

        group.MapGet("/sales", GetProductSalesInDateRange)
            .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString()))
            .WithOpenApi();

        group.MapGet("/most-sold", GetMostSoldProductsInDateRange)
            .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString()))
            .WithOpenApi();

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

    private static async Task<EndpointResult<int>> GetProductSalesInDateRange(
        [FromQuery] Guid productId,
        [AsParameters] DateRangeRequest dateRangeRequest,
        [FromServices] IAdminGetProductSalesInDateRangeUseCase useCase,
        CancellationToken cancellationToken) =>
        new(await useCase.HandleAsync(productId, dateRangeRequest.ToDateRangeQuery(), cancellationToken));

    private static async Task<EndpointResult<ProductsListResponse>> GetMostSoldProductsInDateRange(
        [AsParameters] PaginationRequest paginationRequest,
        [AsParameters] DateRangeRequest dateRangeRequest,
        [FromServices] IAdminGetMostSoldProductsInDateRangeUseCase useCase,
        CancellationToken cancellationToken)
    {
        PaginatedEnumerable<ProductDto> products = await useCase.HandleAsync(paginationRequest.ToPaginationQuery(),
            dateRangeRequest.ToDateRangeQuery(), cancellationToken);

        return new EndpointResult<ProductsListResponse>(new ProductsListResponse(products.Map(p => new ProductResponse(p))));
    }
}