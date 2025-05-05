using System.Text.Json.Serialization;
using Ecommerce.Persistence.Domain.Orders;

namespace Ecommerce.Core.Abstractions.Models.Orders;

[method: JsonConstructor]
public class OrderItemDto(
    Guid productId,
    Guid orderId,
    int quantity,
    decimal pricePerUnit,
    string currencyCode
)
{
    public OrderItemDto(OrderItem item)
        : this(item.ProductId, item.OrderId, item.Quantity, item.PricePerUnit, item.CurrencyCode)
    {
    }

    public Guid ProductId { get; private set; } = productId;

    public Guid OrderId { get; private set; } = orderId;

    public int Quantity { get; private set; } = quantity;

    public decimal PricePerUnit { get; private set; } = pricePerUnit;

    public string CurrencyCode { get; private set; } = currencyCode;
}