using Ecommerce.Domain;

namespace Ecommerce.Infrastructure.Repositories.Users;

public interface IUsersRepository
{
    public Task AddAsync(User user, CancellationToken cancellationToken = default);
}