using Ecommerce.Domain;
using Ecommerce.Infrastructure.Database;

namespace Ecommerce.Infrastructure.Repositories.Users;

internal class UsersRepository(
    UsersDbContext dbContext
) : IUsersRepository
{
    public async Task AddAsync(User user, CancellationToken cancellationToken = default) =>
        await dbContext.AddAsync(user, cancellationToken);
}