namespace Ecommerce.Persistence.Domain.Users;

public class User(
    string email,
    string passwordHash,
    string firstName,
    string lastName,
    bool isEmailConfirmed,
    UserRole role,
    DateTime createdAtUtc
) : ISoftDeletable
{
    public const string TableName = "Users";
    public const int MaxEmailLength = 256;
    public const int MaxNameLength = 256;
    public const int MaxPasswordLength = 60;
    public const int MaxPasswordHashLength = 512;

    public Guid Id { get; init; } = Guid.NewGuid();

    public string Email { get; private set; } = email;

    public string PasswordHash { get; private set; } = passwordHash;

    public string FirstName { get; private set; } = firstName;

    public string LastName { get; private set; } = lastName;

    public bool IsEmailConfirmed { get; private set; } = isEmailConfirmed;

    public DateTime CreatedAtUtc { get; private set; } = createdAtUtc;

    public UserRole Role { get; private set; } = role;

    public bool IsDeleted { get; set; }

    public void SoftDelete() =>
        IsDeleted = true;

    public void ConfirmEmail() =>
        IsEmailConfirmed = true;

    public void MakeAdmin() =>
        Role = UserRole.Admin;
}