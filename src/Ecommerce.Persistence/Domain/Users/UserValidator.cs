using Ecommerce.Extensions.Exceptions;

namespace Ecommerce.Persistence.Domain.Users;

public class UserValidator
{
    public const int MaxEmailLength = 256;
    public const int MaxPasswordLength = 128;
    public const int MaxNameLength = 256;
    public const int MaxPasswordHashLength = 512;

    public static void Validate(User user)
    {
        List<ValidationError> errors = [];

        if (string.IsNullOrWhiteSpace(user.Email))
            errors.Add(new ValidationError("User email cannot be empty."));

        if (user.Email.Length > MaxEmailLength)
            errors.Add(new ValidationError($"User email cannot be longer than {MaxEmailLength} characters."));

        if (string.IsNullOrWhiteSpace(user.PasswordHash))
            errors.Add(new ValidationError("User password hash cannot be empty."));

        if (user.PasswordHash.Length > MaxPasswordHashLength)
            errors.Add(new ValidationError("Password hash is too long."));

        if (string.IsNullOrWhiteSpace(user.FirstName))
            errors.Add(new ValidationError("First name cannot be empty."));

        if (user.FirstName.Length > MaxNameLength)
            errors.Add(new ValidationError($"First name cannot be longer than {MaxNameLength} characters."));

        if (string.IsNullOrWhiteSpace(user.LastName))
            errors.Add(new ValidationError("Last name cannot be empty."));

        if (user.LastName.Length > MaxNameLength)
            errors.Add(new ValidationError($"Last name cannot be longer than {MaxNameLength} characters."));

        ResponseValidationException.ThrowIf(errors.Any, errors);
    }

    public static User CreateOrThrow(
        string email,
        string passwordHash,
        string firstName,
        string lastName,
        bool isEmailConfirmed,
        UserRole role,
        DateTime createdAtUtc
    )
    {
        User user = new(
            email: email,
            passwordHash: passwordHash,
            firstName: firstName,
            lastName: lastName,
            isEmailConfirmed: isEmailConfirmed,
            role: role,
            createdAtUtc: createdAtUtc
        );

        Validate(user);

        return user;
    }

    public static void ValidatePassword(string password)
    {
        List<ValidationError> errors = [];

        if (string.IsNullOrWhiteSpace(password))
            errors.Add(new ValidationError("Password cannot be empty."));

        if (password.Length > MaxPasswordLength)
            errors.Add(new ValidationError($"Password cannot be longer than {MaxPasswordLength} characters."));

        ResponseValidationException.ThrowIf(errors.Any, errors);
    }
}