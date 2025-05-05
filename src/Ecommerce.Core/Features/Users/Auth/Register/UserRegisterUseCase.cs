using Ecommerce.Core.Abstractions.Auth;
using Ecommerce.Persistence.Database;
using Ecommerce.Persistence.Domain.Users;
using Ecommerce.Infrastructure.Time;

namespace Ecommerce.Core.Features.Users.Auth.Register;

public class UserRegisterUseCase(
    ApplicationDbContext dbContext,
    IPasswordHasher passwordHasher,
    IDateTimeProvider dateTimeProvider
) : IUserRegisterUseCase
{
    public async Task HandleAsync(UserRegisterCommand command, CancellationToken cancellationToken = default)
    {
        UserValidator.ValidatePassword(command.Password);

        DateTime utcNow = dateTimeProvider.UtcNow;

        User user = UserValidator.CreateOrThrow(
            email: command.Email,
            passwordHash: passwordHasher.Hash(command.Password),
            firstName: command.FirstName,
            lastName: command.LastName,
            isEmailConfirmed: false,
            role: UserRole.User,
            createdAtUtc: utcNow
        );

        await dbContext.Users.AddAsync(user, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}