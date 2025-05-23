using Ecommerce.Core.Abstractions.Auth;
using Ecommerce.Core.Abstractions.Time;
using Ecommerce.Persistence.Database;
using Ecommerce.Persistence.Domain.Users;

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
            // TODO: email confirmation 
            isEmailConfirmed: true,
            role: UserRole.User,
            createdAtUtc: utcNow
        );

        await dbContext.Users.AddAsync(user, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}