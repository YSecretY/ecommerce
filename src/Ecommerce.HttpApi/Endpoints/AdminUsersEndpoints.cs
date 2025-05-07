using Ecommerce.Core.Features.Products.Analytics.GetMostSoldProducts;
using Ecommerce.Core.Features.Products.Analytics.GetMostViewedProducts;
using Ecommerce.Core.Features.Products.Analytics.GetProductDailySales;
using Ecommerce.Core.Features.Products.Analytics.GetProductSales;
using Ecommerce.Core.Features.Products.Analytics.GetProductTotalStatistics;
using Ecommerce.Core.Features.Products.Analytics.GetTotalSold;
using Ecommerce.Core.Features.Products.Create;
using Ecommerce.Core.Features.Products.DeleteById;
using Ecommerce.Core.Features.Products.DeleteList;
using Ecommerce.Core.Features.Products.Update;
using Ecommerce.Extensions.Requests;
using Ecommerce.Extensions.Types;
using Ecommerce.HttpApi.Contracts;
using Ecommerce.HttpApi.Contracts.Admin.Products;
using Ecommerce.HttpApi.Contracts.Admin.Products.GetMostSoldInDateRange;
using Ecommerce.HttpApi.Contracts.Admin.Products.GetMostViewedInDateRange;
using Ecommerce.HttpApi.Contracts.Products;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.HttpApi.Endpoints;

public static class AdminUsersEndpoints
{
    public static RouteGroupBuilder MapAdminProductsEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("/", CreateProduct).WithOpenApi();
        group.MapPut("/", UpdateProduct).WithOpenApi();
        group.MapDelete("/{id:guid}", DeleteProduct).WithOpenApi();
        group.MapDelete("/", DeleteProductsList).WithOpenApi();
        group.MapGet("/sales", GetProductSalesInDateRange).WithOpenApi();
        group.MapGet("/sales/{productId:guid}", GetProductDailySales).WithOpenApi();
        group.MapGet("/statistics/{productId:guid}", GetProductStatistics).WithOpenApi();
        group.MapGet("/most-sold", GetMostSoldProductsInDateRange).WithOpenApi();
        group.MapGet("/most-viewed", GetMostViewedProductsInDateRange).WithOpenApi();
        group.MapGet("/total-sold", GetTotalProductsSold).WithOpenApi();

        return group;
    }

    private static async Task<EndpointResult<Guid>> CreateProduct(
        [FromBody] AdminCreateProductRequest request,
        [FromServices] IAdminCreateProductUseCase useCase,
        CancellationToken cancellationToken
    ) => new(await useCase.HandleAsync(request.ToCommand(), cancellationToken));

    private static async Task<IResult> UpdateProduct(
        [FromBody] AdminUpdateProductRequest request,
        [FromServices] IAdminUpdateProductUseCase useCase,
        CancellationToken cancellationToken)
    {
        await useCase.HandleAsync(request.ToCommand(), cancellationToken);

        return Results.Ok();
    }

    private static async Task<IResult> DeleteProduct(
        [FromRoute] Guid id,
        [FromServices] IAdminDeleteProductByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        await useCase.HandleAsync(id, cancellationToken);

        return Results.Ok();
    }

    private static async Task<IResult> DeleteProductsList(
        [FromBody] AdminDeleteProductsListRequest request,
        IAdminDeleteProductsListUseCase useCase,
        CancellationToken cancellationToken)
    {
        await useCase.HandleAsync(request.ProductsIds, cancellationToken);

        return Results.Ok();
    }

    private static async Task<EndpointResult<int>> GetProductSalesInDateRange(
        [FromQuery] Guid productId,
        [AsParameters] DateRangeRequest dateRangeRequest,
        [FromServices] IAdminGetProductSalesInDateRangeUseCase useCase,
        CancellationToken cancellationToken) =>
        new(await useCase.HandleAsync(productId, dateRangeRequest.ToDateRangeQuery(), cancellationToken));

    private static async Task<EndpointResult<AdminGetMostSoldProductsInDateRangeResponse>> GetMostSoldProductsInDateRange(
        [AsParameters] PaginationRequest paginationRequest,
        [AsParameters] DateRangeRequest dateRangeRequest,
        [FromServices] IAdminGetMostSoldProductsInDateRangeUseCase useCase,
        CancellationToken cancellationToken)
    {
        PaginatedEnumerable<ProductWithSalesInfoDto> products = await useCase.HandleAsync(
            paginationRequest.ToPaginationQuery(),
            dateRangeRequest.ToDateRangeQuery(), cancellationToken);

        PaginatedResult<ProductWithSalesInfoResponse> response = new(products.Map(p => new ProductWithSalesInfoResponse(p)));

        return new EndpointResult<AdminGetMostSoldProductsInDateRangeResponse>(
            new AdminGetMostSoldProductsInDateRangeResponse(response)
        );
    }

    private static async Task<EndpointResult<AdminGetMostViewedProductsInDateRangeResponse>>
        GetMostViewedProductsInDateRange(
            [AsParameters] PaginationRequest paginationRequest,
            [AsParameters] DateRangeRequest dateRangeRequest,
            [FromServices] IAdminGetMostViewedProductsInDateRangeUseCase useCase,
            CancellationToken cancellationToken
        )
    {
        PaginatedEnumerable<ProductWithViewsInfoDto> products = await useCase.HandleAsync(
            paginationRequest.ToPaginationQuery(), dateRangeRequest.ToDateRangeQuery(), cancellationToken);

        PaginatedResult<ProductWithViewsInfoResponse> response = new(products.Map(p => new ProductWithViewsInfoResponse(p)));

        return new EndpointResult<AdminGetMostViewedProductsInDateRangeResponse>(
            new AdminGetMostViewedProductsInDateRangeResponse(response));
    }

    private static async Task<EndpointResult<int>> GetProductDailySales(
        [FromRoute] Guid productId,
        [FromQuery] DateOnly date,
        [FromServices] IAdminGetProductDailySalesUseCase useCase,
        CancellationToken cancellationToken) => new(await useCase.HandleAsync(productId, date, cancellationToken));

    private static async Task<EndpointResult<ProductStatisticsResponse>> GetProductStatistics(
        [FromRoute] Guid productId,
        [FromServices] IAdminGetProductTotalStatisticsUseCase useCase,
        CancellationToken cancellationToken) =>
        new(new ProductStatisticsResponse(await useCase.HandleAsync(productId, cancellationToken)));

    private static async Task<EndpointResult<long>> GetTotalProductsSold(
        [FromServices] IAdminGetTotalProductsSoldUseCase useCase, CancellationToken cancellationToken) =>
        new(await useCase.HandleAsync(cancellationToken));
}