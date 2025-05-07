using Ecommerce.Core.Abstractions.Models.Orders;
using Ecommerce.Core.Features.Orders.GetList;
using Ecommerce.Extensions.Requests;
using Ecommerce.Extensions.Types;
using Ecommerce.HttpApi.Contracts;
using Ecommerce.HttpApi.Contracts.Orders;
using Ecommerce.Persistence.Domain.Orders;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.HttpApi.Endpoints;

public static class AdminOrdersEndpoints
{
    public static RouteGroupBuilder MapAdminOrdersEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetOrders);

        return group;
    }

    public static async Task<EndpointResult<PaginatedResult<OrderResponse>>> GetOrders(
        [AsParameters] PaginationRequest paginationRequest,
        [FromQuery] string? status,
        [FromServices] IAdminGetOrdersUseCase useCase,
        CancellationToken cancellationToken)
    {
        PaginatedEnumerable<OrderDto> orders = await useCase.HandleAsync(paginationRequest.ToPaginationQuery(),
            status?.ToEnum<OrderStatus>(), cancellationToken);

        return new EndpointResult<PaginatedResult<OrderResponse>>(
            new PaginatedResult<OrderResponse>(orders.Map(o => new OrderResponse(o)))
        );
    }
}