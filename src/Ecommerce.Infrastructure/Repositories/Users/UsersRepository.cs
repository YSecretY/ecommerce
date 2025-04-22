using Ecommerce.Domain.Users;
using Ecommerce.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories.Users;

internal class UsersRepository(
    UsersDbContext dbContext
) : IUsersRepository
{
    public async Task AddAsync(User user, CancellationToken cancellationToken = default) =>
        await dbContext.AddAsync(user, cancellationToken);

    public async Task<bool> ExistsAsync(string email, CancellationToken cancellationToken = default) =>
        await dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken) is not null;

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default) =>
        await dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
}