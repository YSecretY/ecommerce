using Ecommerce.Domain;
using Ecommerce.Domain.Users;
using Ecommerce.Extensions.Exceptions;

namespace Ecommerce.Core.Auth.Shared.Internal.Validators;

internal class NameValidator
{
    public static ValidationResult Validate(string name)
    {
        List<ValidationError> errors = [];

        if (string.IsNullOrWhiteSpace(name))
            errors.Add(new ValidationError("Name cannot be empty."));

        if (name.Length > User.MaxNameLength)
            errors.Add(new ValidationError($"Name cannot be longer than {User.MaxNameLength} characters."));

        return new ValidationResult(errors);
    }
}