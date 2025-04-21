using Ecommerce.Core.Auth.Login;
using Ecommerce.Core.Auth.Register;
using Ecommerce.Core.Auth.Shared;

namespace Ecommerce.Core.Auth;

public interface IAuthService
{
    public Task<IdentityToken> RegisterAsync(RegisterUserCommand command, CancellationToken cancellationToken = default);

    public Task<IdentityToken> LoginAsync(LoginUserCommand command, CancellationToken cancellationToken = default);
}