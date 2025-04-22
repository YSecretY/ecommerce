using Ecommerce.Extensions.Exceptions;

namespace Ecommerce.Domain.Products;

public class ProductValidator
{
    public const int MaxNameLength = 512;
    public const int MaxDescriptionLength = 2056;
    public const int MaxSkuLength = 256;
    public const int MaxBrandLength = 256;
    public const int MaxImageUrlLength = 2056;
    public const int MaxCurrencyCodeLength = 3;
    public const int MaxCountryCodeLength = 3;

    public static ValidationResult Validate(Product product)
    {
        List<ValidationError> errors = [];

        if (string.IsNullOrWhiteSpace(product.Name))
            errors.Add(new ValidationError("Product name cannot be empty."));

        if (product.Name.Length > MaxNameLength)
            errors.Add(new ValidationError($"Product name cannot be longer than {MaxNameLength}"));

        if (string.IsNullOrWhiteSpace(product.Description))
            errors.Add(new ValidationError("Product description cannot be empty."));

        if (product.Description.Length > MaxDescriptionLength)
            errors.Add(new ValidationError($"Product description cannot be longer than {MaxDescriptionLength}"));

        if (string.IsNullOrWhiteSpace(product.Sku))
            errors.Add(new ValidationError("Product sku cannot be empty."));

        if (product.Sku.Length > MaxSkuLength)
            errors.Add(new ValidationError($"Product sku cannot be longer than {MaxSkuLength}"));

        if (string.IsNullOrWhiteSpace(product.Brand))
            errors.Add(new ValidationError("Product brand cannot be empty."));

        if (product.Brand.Length > MaxBrandLength)
            errors.Add(new ValidationError($"Product brand cannot be longer than {MaxBrandLength}"));

        if (string.IsNullOrWhiteSpace(product.MainImageUrl))
            errors.Add(new ValidationError("Product mainImageUrl cannot be empty."));

        if (product.MainImageUrl.Length > MaxImageUrlLength)
            errors.Add(new ValidationError($"Product mainImageUrl cannot be longer than {MaxImageUrlLength}"));

        if (string.IsNullOrWhiteSpace(product.CurrencyCode))
            errors.Add(new ValidationError("Product currencyCode cannot be empty."));

        if (product.CurrencyCode.Length > MaxCurrencyCodeLength)
            errors.Add(new ValidationError($"Product currencyCode cannot be longer than {MaxCurrencyCodeLength}"));

        if (string.IsNullOrWhiteSpace(product.CountryCode))
            errors.Add(new ValidationError("Product countryCode cannot be empty."));

        if (product.CountryCode.Length > MaxCountryCodeLength)
            errors.Add(new ValidationError($"Product countryCode cannot be longer than {MaxCountryCodeLength}"));

        return new ValidationResult(errors);
    }
}