using System.Text.Json.Serialization;
using Ecommerce.Persistence.Domain.Orders;

namespace Ecommerce.Core.Abstractions.Models.Orders;

[method: JsonConstructor]
public record AddressDto(
    string Street,
    string City,
    string PostalCode,
    string CountryCode
)
{
    public AddressDto(Address address)
        : this(address.Street, address.City, address.PostalCode, address.CountryCode)
    {
    }

    public Address ToEntity() =>
        new(Street, City, PostalCode, CountryCode);
};