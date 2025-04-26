using System.ComponentModel.DataAnnotations;
using Ecommerce.Persistence.Domain.Orders;
using Ecommerce.Persistence.Domain.Reviews;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
    private const string TableName = "Users";

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

    public ICollection<Order> Orders { get; private set; } = null!;

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

    public static void Builder(EntityTypeBuilder<User> user)
    {
        user.ToTable(TableName);

        user.HasKey(u => u.Id);

        user.HasIndex(u => u.Email).IsUnique();

        user.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(UserValidator.MaxEmailLength);

        user.Property(u => u.PasswordHash)
            .IsRequired()
            .HasMaxLength(UserValidator.MaxPasswordHashLength);

        user.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(UserValidator.MaxNameLength);

        user.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(UserValidator.MaxNameLength);

        user.Property(u => u.IsEmailConfirmed)
            .IsRequired();

        user.Property(u => u.Role)
            .IsRequired();

        user.Property(u => u.CreatedAtUtc)
            .IsRequired();

        user.HasMany(u => u.Reviews)
            .WithOne(r => r.User)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}