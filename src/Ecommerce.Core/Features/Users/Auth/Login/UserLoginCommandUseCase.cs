using Ecommerce.Extensions.Exceptions;
using Ecommerce.Infrastructure.Auth.Abstractions;
using Ecommerce.Persistence.Database;
using Ecommerce.Persistence.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Core.Features.Users.Auth.Login;

public class UserLoginCommandUseCase(
    ApplicationDbContext dbContext,
    IPasswordHasher passwordHasher,
    IIdentityTokenGenerator identityTokenGenerator
) : IUserLoginCommandUseCase
{
    public async Task<IdentityToken> HandleAsync(UserLoginCommand command, CancellationToken cancellationToken = default)
    {
        User user = await dbContext.Users
                        .FirstOrDefaultAsync(u => u.Email == command.Email, cancellationToken)
                    ?? throw new ForbiddenException();

        if (!passwordHasher.IsValid(command.Password, user.PasswordHash))
            throw new ForbiddenException();

        return identityTokenGenerator.Generate(user);
    }
}