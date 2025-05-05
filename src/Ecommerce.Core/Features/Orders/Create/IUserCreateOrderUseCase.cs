namespace Ecommerce.Core.Features.Orders.Create;

public interface IUserCreateOrderUseCase
{
    public Task<Guid> HandleAsync(UserCreateOrderCommand command, CancellationToken cancellationToken = default);
}