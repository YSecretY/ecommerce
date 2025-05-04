using Ecommerce.Core.Abstractions.Auth;

namespace Ecommerce.Core.Features.Users.Auth.Login;

public interface IUserLoginCommandUseCase
{
    public Task<IdentityToken> HandleAsync(UserLoginCommand command, CancellationToken cancellationToken = default);
}