namespace Ecommerce.Infrastructure.Database.Users;

public interface IUsersUnitOfWork
{
    public Task SaveChangesAsync(CancellationToken cancellationToken = default);
}