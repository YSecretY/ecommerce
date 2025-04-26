namespace Ecommerce.Core.Features.Users.Auth.Register;

public interface IUserRegisterUseCase
{
    public Task HandleAsync(UserRegisterCommand command, CancellationToken cancellationToken = default);
}