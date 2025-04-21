using Ecommerce.Domain;

namespace Ecommerce.Infrastructure.Repositories.Users;

public interface IUsersRepository
{
    public Task AddAsync(User user, CancellationToken cancellationToken = default);

    public Task<bool> ExistsAsync(string email, CancellationToken cancellationToken = default);

    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}