using System.Text.Json.Serialization;
using Ecommerce.Persistence.Domain.Orders;

namespace Ecommerce.Core.Abstractions.Models.Orders;

[method: JsonConstructor]
public class OrderDto(
    Guid orderId,
    Guid userId,
    List<OrderItemDto> items,
    string currencyCode,
    AddressDto address,
    OrderStatus status,
    DateTime createdAtUtc,
    DateTime updatedAtUtc
)
{
    public OrderDto(Order order)
        : this(
            order.Id,
            order.UserId,
            order.OrderItems.Select(i => new OrderItemDto(i)).ToList(),
            order.CurrencyCode,
            new AddressDto(order.Address),
            order.Status,
            order.CreatedAtUtc,
            order.UpdatedAtUtc
        )
    {
    }

    public Guid OrderId { get; private set; } = orderId;

    public Guid UserId { get; private set; } = userId;

    public List<OrderItemDto> Items { get; private set; } = items;

    public string CurrencyCode { get; private set; } = currencyCode;

    public AddressDto Address { get; private set; } = address;

    public OrderStatus Status { get; private set; } = status;

    public DateTime CreatedAtUtc { get; private set; } = createdAtUtc;

    public DateTime UpdatedAtUtc { get; private set; } = updatedAtUtc;
}