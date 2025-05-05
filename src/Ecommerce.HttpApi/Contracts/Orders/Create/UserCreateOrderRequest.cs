using System.Text.Json.Serialization;
using Ecommerce.Core.Features.Orders.Create;

namespace Ecommerce.HttpApi.Contracts.Orders.Create;

public class UserCreateOrderRequest
{
    [JsonPropertyName("orderItems")]
    public List<OrderItemRequestModel> OrderItems { get; set; } = null!;

    [JsonPropertyName("address")]
    public AddressRequestModel Address { get; set; } = null!;

    [JsonPropertyName("currencyCode")]
    public string CurrencyCode { get; set; } = string.Empty;

    public UserCreateOrderCommand ToCommand() =>
        new(OrderItems.Select(oi => oi.ToCommandModel()).ToList(), Address.ToDto(), CurrencyCode);
}