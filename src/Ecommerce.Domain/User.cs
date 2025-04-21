namespace Ecommerce.Domain;

public class User(
    string email,
    string passwordHash,
    string firstName,
    string lastName,
    bool isEmailConfirmed,
    UserRole role,
    DateTime createdAtUtc
)
{
    public const string TableName = "Users";

    public Guid Id { get; init; }

    public string Email { get; private set; } = email;

    public string PasswordHash { get; private set; } = passwordHash;

    public string FirstName { get; private set; } = firstName;

    public string LastName { get; private set; } = lastName;

    public bool IsEmailConfirmed { get; private set; } = isEmailConfirmed;

    public DateTime CreatedAtUtc { get; private set; } = createdAtUtc;

    public UserRole Role { get; private set; } = role;

    public void ConfirmEmail() =>
        IsEmailConfirmed = true;

    public void MakeAdmin() =>
        Role = UserRole.Admin;
}