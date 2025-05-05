using Ecommerce.Persistence.Domain.Orders;

namespace Ecommerce.Core.Features.Orders;

public record AddressDto(
    string Street,
    string City,
    string PostalCode,
    string CountryCode
)
{
    public Address ToEntity() =>
        new Address(Street, City, PostalCode, CountryCode);
};