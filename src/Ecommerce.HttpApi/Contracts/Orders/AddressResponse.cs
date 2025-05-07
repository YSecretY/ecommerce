using System.Text.Json.Serialization;
using Ecommerce.Core.Abstractions.Models.Orders;

namespace Ecommerce.HttpApi.Contracts.Orders;

public class AddressResponse(
    string street,
    string city,
    string postalCode,
    string countryCode
)
{
    public AddressResponse(AddressDto dto)
        : this(dto.Street, dto.City, dto.PostalCode, dto.CountryCode)
    {
    }

    [JsonPropertyName("Street")]
    public string Street { get; private set; } = street;

    [JsonPropertyName("City")]
    public string City { get; private set; } = city;

    [JsonPropertyName("PostalCode")]
    public string PostalCode { get; private set; } = postalCode;

    [JsonPropertyName("CountryCode")]
    public string CountryCode { get; private set; } = countryCode;
};