namespace Ecommerce.Infrastructure.Database.Users;

internal class UsersUnitOfWork(
    UsersDbContext dbContext
) : IUsersUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await dbContext.SaveChangesAsync(cancellationToken);
}