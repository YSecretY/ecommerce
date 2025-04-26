using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ecommerce.Persistence.Domain.Orders.Validators;
using Ecommerce.Persistence.Domain.Products;

namespace Ecommerce.Persistence.Domain.Orders;

[ComplexType]
public class Address(
    string street,
    string city,
    string postalCode,
    string countryCode
)
{
    [MaxLength(AddressValidator.MaxStreetLength)]
    public string Street { get; private set; } = street;

    [MaxLength(AddressValidator.MaxCityLength)]
    public string City { get; private set; } = city;

    [MaxLength(AddressValidator.MaxPostalCodeLength)]
    public string PostalCode { get; private set; } = postalCode;

    [MaxLength(ProductValidator.MaxCountryCodeLength)]
    public string CountryCode { get; private set; } = countryCode;
}