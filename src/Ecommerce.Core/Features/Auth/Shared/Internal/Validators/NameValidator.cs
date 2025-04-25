using Ecommerce.Extensions.Exceptions;
using Ecommerce.Persistence.Domain.Users;

namespace Ecommerce.Core.Features.Auth.Shared.Internal.Validators;

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