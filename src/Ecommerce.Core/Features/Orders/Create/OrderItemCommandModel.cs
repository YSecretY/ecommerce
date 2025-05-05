namespace Ecommerce.Core.Features.Orders.Create;

public record OrderItemCommandModel(
    Guid ProductId,
    int Quantity
);