using Ecommerce.Persistence.Database;
using Ecommerce.Persistence.Domain.Users;
using Ecommerce.Infrastructure.Auth.Abstractions;
using Ecommerce.Infrastructure.Time;

namespace Ecommerce.Core.Features.Users.Auth.Register;

public class UserRegisterUseCase(
    UsersDbContext dbContext,
    IPasswordHasher passwordHasher,
    IDateTimeProvider dateTimeProvider
) : IUserRegisterUseCase
{
    public async Task HandleAsync(UserRegisterCommand command, CancellationToken cancellationToken = default)
    {
        UserValidator.ValidatePassword(command.Password);

        DateTime utcNow = dateTimeProvider.UtcNow;

        User user = UserValidator.CreateValid(
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