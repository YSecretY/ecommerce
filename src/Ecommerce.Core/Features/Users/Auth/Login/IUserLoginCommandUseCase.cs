using Ecommerce.Infrastructure.Auth.Abstractions;

namespace Ecommerce.Core.Features.Users.Auth.Login;

public interface IUserLoginCommandUseCase
{
    public Task<IdentityToken> HandleAsync(UserLoginCommand command, CancellationToken cancellationToken = default);
}