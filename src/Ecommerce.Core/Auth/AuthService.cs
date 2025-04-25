using Ecommerce.Core.Auth.Login;
using Ecommerce.Core.Auth.Register;
using Ecommerce.Core.Auth.Shared;
using Ecommerce.Core.Auth.Shared.Internal;
using Ecommerce.Extensions.Exceptions;
using Ecommerce.Extensions.Time;
using Ecommerce.Persistence.Database;
using Ecommerce.Persistence.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Core.Auth;

internal class AuthService(
    UsersDbContext usersDbContext,
    IPasswordHasher passwordHasher,
    IDateTimeProvider dateTimeProvider,
    IIdentityTokenGenerator identityTokenGenerator
) : IAuthService
{
    public async Task<IdentityToken> RegisterAsync(RegisterUserCommand command,
        CancellationToken cancellationToken = default)
    {
        if (await usersDbContext.Users.AnyAsync(u => u.Email == command.Email, cancellationToken))
            throw new UnauthorizedException("User already exists.");

        User user = new(
            email: command.Email,
            passwordHash: passwordHasher.Hash(command.Password),
            firstName: command.FirstName,
            lastName: command.LastName,
            isEmailConfirmed: false,
            role: UserRole.User,
            createdAtUtc: dateTimeProvider.UtcNow
        );

        await usersDbContext.Users.AddAsync(user, cancellationToken);
        await usersDbContext.SaveChangesAsync(cancellationToken);

        return identityTokenGenerator.Generate(user);
    }

    public async Task<IdentityToken> LoginAsync(LoginUserCommand command, CancellationToken cancellationToken = default)
    {
        User user = await usersDbContext.Users.FirstOrDefaultAsync(u => u.Email == command.Email, cancellationToken)
                    ?? throw new UnauthorizedException();

        UnauthorizedException.ThrowIf(() => !passwordHasher.IsValid(command.Password, user.PasswordHash));

        return identityTokenGenerator.Generate(user);
    }
}