using System.Text.Json.Serialization;
using Ecommerce.Core.Abstractions.Models.Orders;
using Ecommerce.Persistence.Domain.Orders;

namespace Ecommerce.HttpApi.Contracts.Orders;

public class OrderResponse(
    Guid orderId,
    Guid userId,
    List<OrderItemResponse> items,
    string currencyCode,
    AddressResponse address,
    OrderStatus status,
    DateTime createdAtUtc,
    DateTime updatedAtUtc
)
{
    public OrderResponse(OrderDto dto)
        : this(
            dto.OrderId,
            dto.UserId,
            dto.Items.Select(i => new OrderItemResponse(i)).ToList(),
            dto.CurrencyCode,
            new AddressResponse(dto.Address),
            dto.Status,
            dto.CreatedAtUtc,
            dto.UpdatedAtUtc
        )
    {
    }

    [JsonPropertyName("orderId")]
    public Guid OrderId { get; private set; } = orderId;

    [JsonPropertyName("userId")]
    public Guid UserId { get; private set; } = userId;

    [JsonPropertyName("items")]
    public List<OrderItemResponse> Items { get; private set; } = items;

    [JsonPropertyName("currencyCode")]
    public string CurrencyCode { get; private set; } = currencyCode;

    [JsonPropertyName("address")]
    public AddressResponse Address { get; private set; } = address;

    [JsonPropertyName("status")]
    public string Status { get; private set; } = status.ToString();

    [JsonPropertyName("createdAtUtc")]
    public DateTime CreatedAtUtc { get; private set; } = createdAtUtc;

    [JsonPropertyName("updatedAtUtc")]
    public DateTime UpdatedAtUtc { get; private set; } = updatedAtUtc;
}