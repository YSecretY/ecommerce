using Ecommerce.Core.Features.Products.Create;
using Ecommerce.Core.Features.Products.DeleteById;
using Ecommerce.Core.Features.Products.DeleteList;
using Ecommerce.Core.Features.Products.Update;
using Ecommerce.Extensions.Requests;
using Ecommerce.HttpApi.Contracts.Admin.Products;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.HttpApi.Endpoints;

public static class AdminEndpoints
{
    public static RouteGroupBuilder MapAdminEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("/products", CreateProduct).WithOpenApi();
        group.MapPut("/products", UpdateProduct).WithOpenApi();
        group.MapDelete("/products/{id:guid}", DeleteProduct).WithOpenApi();
        group.MapDelete("/products", DeleteProductsList).WithOpenApi();

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
}