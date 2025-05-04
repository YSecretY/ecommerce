namespace Ecommerce.Core.Abstractions.Events.Products;

public class ProductViewedEvent(Guid productId, Guid userId, DateTime occuredAtUtc) : EventBase(occuredAtUtc)
{
    public Guid ProductId { get; private set; } = productId;

    public Guid UserId { get; private set; } = userId;
}