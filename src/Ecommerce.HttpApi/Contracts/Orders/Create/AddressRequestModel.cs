using System.Text.Json.Serialization;
using Ecommerce.Core.Abstractions.Models.Orders;
using Ecommerce.Core.Features.Orders;

namespace Ecommerce.HttpApi.Contracts.Orders.Create;

public class AddressRequestModel
{
    [JsonPropertyName("street")]
    public string Street { get; set; } = string.Empty;

    [JsonPropertyName("city")]
    public string City { get; set; } = string.Empty;

    [JsonPropertyName("postalCode")]
    public string PostalCode { get; set; } = string.Empty;

    [JsonPropertyName("countryCode")]
    public string CountryCode { get; set; } = string.Empty;

    public AddressDto ToDto() =>
        new(Street, City, PostalCode, CountryCode);
}