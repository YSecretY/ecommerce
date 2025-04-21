using Ecommerce.Core.Auth.Register;

namespace Ecommerce.Core.Auth;

public interface IAuthService
{
    public Task RegisterAsync(RegisterUserCommand command, CancellationToken cancellationToken = default);
}