using System.Text.Json.Serialization;
using Ecommerce.Core.Abstractions.Models.Orders;

namespace Ecommerce.HttpApi.Contracts.Orders;

public class OrderItemResponse(
    Guid productId,
    Guid orderId,
    int quantity,
    decimal pricePerUnit,
    string currencyCode
)
{
    public OrderItemResponse(OrderItemDto dto)
        : this(dto.ProductId, dto.OrderId, dto.Quantity, dto.PricePerUnit, dto.CurrencyCode)
    {
    }

    [JsonPropertyName("productId")]
    public Guid ProductId { get; private set; } = productId;

    [JsonPropertyName("orderId")]
    public Guid OrderId { get; private set; } = orderId;

    [JsonPropertyName("quantity")]
    public int Quantity { get; private set; } = quantity;

    [JsonPropertyName("pricePerUnit")]
    public decimal PricePerUnit { get; private set; } = pricePerUnit;

    [JsonPropertyName("currencyCode")]
    public string CurrencyCode { get; private set; } = currencyCode;
}