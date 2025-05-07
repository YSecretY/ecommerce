using Ecommerce.Core.Abstractions.Models.Products;
using Ecommerce.Core.Features.Users.Analytics.GetMostViewedProducts;
using Ecommerce.Extensions.Requests;
using Ecommerce.HttpApi.Contracts.Products;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.HttpApi.Endpoints;

public static class UsersEndpoints
{
    public static RouteGroupBuilder MapUsersEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/users/most-viewed-products", GetUserMostViewedProductsResponse).WithOpenApi();

        return group;
    }

    private static async Task<ActionResult<EndpointResult<List<ProductResponse>>>>
        GetUserMostViewedProductsResponse(
            [FromQuery] int count,
            [FromServices] IUserGetMostViewedProductsUseCase useCase,
            CancellationToken cancellationToken = default)
    {
        List<ProductDto> products = await useCase.HandleAsync(count, cancellationToken);

        return new EndpointResult<List<ProductResponse>>(products.Select(p => new ProductResponse(p)).ToList());
    }
}