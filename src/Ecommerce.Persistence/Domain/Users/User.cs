using System.ComponentModel.DataAnnotations;
using Ecommerce.Persistence.Domain.Reviews;

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

    public Guid Id { get; init; } = Guid.NewGuid();

    [MaxLength(UserValidator.MaxEmailLength)]
    public string Email { get; private set; } = email;

    [MaxLength(UserValidator.MaxPasswordHashLength)]
    public string PasswordHash { get; private set; } = passwordHash;

    [MaxLength(UserValidator.MaxNameLength)]
    public string FirstName { get; private set; } = firstName;

    [MaxLength(UserValidator.MaxNameLength)]
    public string LastName { get; private set; } = lastName;

    public bool IsEmailConfirmed { get; private set; } = isEmailConfirmed;

    public DateTime CreatedAtUtc { get; private set; } = createdAtUtc;

    public UserRole Role { get; private set; } = role;

    public bool IsDeleted { get; set; }

    public ICollection<ProductReview> Reviews { get; private set; } = null!;

    public ICollection<ProductReviewReply> Replies { get; private set; } = null!;

    public void SoftDelete()
    {
        IsDeleted = true;

        foreach (ProductReview review in Reviews)
            review.SoftDelete();
    }

    public void ConfirmEmail() =>
        IsEmailConfirmed = true;

    public void MakeAdmin() =>
        Role = UserRole.Admin;
}