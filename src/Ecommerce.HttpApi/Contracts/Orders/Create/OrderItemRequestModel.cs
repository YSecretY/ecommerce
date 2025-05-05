using System.Text.Json.Serialization;
using Ecommerce.Core.Features.Orders.Create;

namespace Ecommerce.HttpApi.Contracts.Orders.Create;

public class OrderItemRequestModel
{
    [JsonPropertyName("productId")]
    public Guid ProductId { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    public OrderItemCommandModel ToCommandModel() =>
        new(ProductId, Quantity);
}