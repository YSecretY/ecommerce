using Ecommerce.Core.Features.Auth.Login;
using Ecommerce.Core.Features.Auth.Register;
using Ecommerce.Core.Features.Auth.Shared;

namespace Ecommerce.Core.Features.Auth;

public interface IAuthService
{
    public Task<IdentityToken> RegisterAsync(RegisterUserCommand command, CancellationToken cancellationToken = default);

    public Task<IdentityToken> LoginAsync(LoginUserCommand command, CancellationToken cancellationToken = default);
}