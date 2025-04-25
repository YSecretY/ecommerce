using Ecommerce.Core.Features.Auth.Shared.Internal.Validators;
using Ecommerce.Extensions.Exceptions;

namespace Ecommerce.Core.Features.Auth.Register;

public class RegisterUserCommand
{
    public RegisterUserCommand(string email, string password, string firstName, string lastName)
    {
        ValidationResult emailValidation = EmailValidator.Validate(email);
        ValidationResult passwordValidation = PasswordValidator.Validate(password);
        ValidationResult firstNameValidation = NameValidator.Validate(firstName);
        ValidationResult lastNameValidation = NameValidator.Validate(lastName);

        ResponseValidationException.ThrowIf(emailValidation.Failed, emailValidation);
        ResponseValidationException.ThrowIf(passwordValidation.Failed, passwordValidation);
        ResponseValidationException.ThrowIf(firstNameValidation.Failed, firstNameValidation);
        ResponseValidationException.ThrowIf(lastNameValidation.Failed, lastNameValidation);

        Email = email;
        Password = password;
        FirstName = firstName;
        LastName = lastName;
    }

    public string Email { get; private set; }

    public string Password { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }
}