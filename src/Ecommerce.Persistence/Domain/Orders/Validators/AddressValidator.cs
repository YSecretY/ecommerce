using Ecommerce.Extensions.Exceptions;
using Ecommerce.Persistence.Domain.Products;

namespace Ecommerce.Persistence.Domain.Orders.Validators;

public class AddressValidator
{
    public const int MaxPostalCodeLength = 50;
    public const int MaxCityLength = 512;
    public const int MaxStreetLength = 512;

    public static void Validate(Address address)
    {
        List<ValidationError> errors = [];

        if (string.IsNullOrWhiteSpace(address.CountryCode))
            errors.Add(new ValidationError("CountryCode is required and cannot be empty."));

        if (address.CountryCode.Length > ProductValidator.MaxCountryCodeLength)
            errors.Add(new ValidationError($"CountryCode cannot exceed {ProductValidator.MaxCountryCodeLength}."));

        if (string.IsNullOrWhiteSpace(address.City))
            errors.Add(new ValidationError("City is required and cannot be empty."));

        if (address.City.Length > MaxCityLength)
            errors.Add(new ValidationError($"City cannot exceed {MaxCityLength}."));

        if (string.IsNullOrWhiteSpace(address.Street))
            errors.Add(new ValidationError("Street is required and cannot be empty."));

        if (address.Street.Length > MaxStreetLength)
            errors.Add(new ValidationError($"Street cannot exceed {MaxStreetLength}."));

        if (string.IsNullOrWhiteSpace(address.PostalCode))
            errors.Add(new ValidationError("PostalCode is required and cannot be empty."));

        if (address.PostalCode.Length > MaxPostalCodeLength)
            errors.Add(new ValidationError($"PostalCode cannot exceed {MaxPostalCodeLength}."));

        ResponseValidationException.ThrowIf(errors.Any, errors);
    }
}