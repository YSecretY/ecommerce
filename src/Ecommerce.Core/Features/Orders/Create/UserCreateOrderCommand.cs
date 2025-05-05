using Ecommerce.Core.Abstractions.Models.Orders;

namespace Ecommerce.Core.Features.Orders.Create;

public record UserCreateOrderCommand(
    List<OrderItemCommandModel> OrderItems,
    AddressDto Address,
    string CurrencyCode
);