using System.Text.Json.Serialization;

namespace Ecommerce.Core.Abstractions.Events.Products;

[method: JsonConstructor]
public class ProductViewedEvent(Guid productId, Guid userId, DateTime occuredAtUtc)
    : EventBase(occuredAtUtc), IHasQueueName
{
    [JsonPropertyName("productId")]
    public Guid ProductId { get; private set; } = productId;

    [JsonPropertyName("userId")]
    public Guid UserId { get; private set; } = userId;

    public static string QueueName => "product-viewed";
}