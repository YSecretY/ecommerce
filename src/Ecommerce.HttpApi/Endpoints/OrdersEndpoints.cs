using Ecommerce.Core.Features.Orders.Create;
using Ecommerce.Extensions.Requests;
using Ecommerce.HttpApi.Contracts.Orders.Create;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.HttpApi.Endpoints;

public static class OrdersEndpoints
{
    public static RouteGroupBuilder MapOrdersEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("/", Create).WithOpenApi();

        return group;
    }

    private static async Task<EndpointResult<Guid>> Create(
        [FromBody] UserCreateOrderRequest request,
        [FromServices] IUserCreateOrderUseCase useCase,
        CancellationToken cancellationToken
    ) => new(await useCase.HandleAsync(request.ToCommand(), cancellationToken));
}