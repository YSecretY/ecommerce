using Ecommerce.Core.Auth.Login;
using Ecommerce.Core.Auth.Register;
using Ecommerce.Core.Auth.Register.Internal;
using Ecommerce.Domain;
using Ecommerce.Extensions.Exceptions;
using Ecommerce.Extensions.Time;
using Ecommerce.Infrastructure.Database.Users;
using Ecommerce.Infrastructure.Repositories.Users;

namespace Ecommerce.Core.Auth;

internal class AuthService(
    IUsersRepository usersRepository,
    IUsersUnitOfWork unitOfWork,
    IPasswordHasher passwordHasher,
    IDateTimeProvider dateTimeProvider
) : IAuthService
{
    public async Task RegisterAsync(RegisterUserCommand command, CancellationToken cancellationToken = default)
    {
        if (await usersRepository.ExistsAsync(command.Email, cancellationToken))
            throw new UnauthorizedAccessException("User already exists.");

        User user = new(
            email: command.Email,
            passwordHash: passwordHasher.Hash(command.Password),
            firstName: command.FirstName,
            lastName: command.LastName,
            isEmailConfirmed: false,
            role: UserRole.User,
            createdAtUtc: dateTimeProvider.UtcNow
        );

        await usersRepository.AddAsync(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<AuthToken> LoginAsync(LoginUserCommand command, CancellationToken cancellationToken = default)
    {
        User user = await usersRepository.GetByEmailAsync(command.Email, cancellationToken)
                    ?? throw new UnauthorizedException();

        UnauthorizedException.ThrowIf(() => !passwordHasher.IsValid(command.Password, user.PasswordHash));

        throw new NotImplementedException();
    }
}