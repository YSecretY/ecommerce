using Ecommerce.Domain;
using Ecommerce.Extensions.Exceptions;

namespace Ecommerce.Core.Auth.Register.Internal.Validators;

internal class PasswordValidator
{
    public static ValidationResult Validate(string password)
    {
        List<ValidationError> errors = [];

        if (password.Length < 6)
            errors.Add(new ValidationError("Password must be at least 6 characters."));

        if (password.Length > User.MaxPasswordLength)
            errors.Add(new ValidationError("Password must be at most 60 characters."));

        if (!password.Any(char.IsUpper))
            errors.Add(new ValidationError("Password must contain at least one uppercase letter."));

        if (!password.Any(char.IsLower))
            errors.Add(new ValidationError("Password must contain at least one lowercase letter."));

        if (!password.Any(char.IsDigit))
            errors.Add(new ValidationError("Password must contain at least one number."));

        return new ValidationResult(errors);
    }
}