using System.ComponentModel.DataAnnotations;
using Ecommerce.Extensions.Exceptions;
using ValidationResult = Ecommerce.Extensions.Exceptions.ValidationResult;

namespace Ecommerce.Core.Auth.Register.Internal.Validators;

internal class EmailValidator
{
    public static ValidationResult Validate(string email)
    {
        List<ValidationError> errors = [];

        EmailAddressAttribute emailValidation = new();

        if (!emailValidation.IsValid(email))
            errors.Add(new ValidationError("Invalid email address."));

        return new ValidationResult(errors);
    }
}