using System.Text.Json.Serialization;
using Ecommerce.Core.Abstractions.Models.Orders;

namespace Ecommerce.Core.Abstractions.Events.Orders;

[method: JsonConstructor]
public class OrderCreatedEvent(OrderDto orderDto, DateTime occuredAtUtc) : EventBase(occuredAtUtc), IHasQueueName
{
    [JsonPropertyName("orderDto")]
    public OrderDto OrderDto { get; private set; } = orderDto;

    public static string QueueName => "order-created";
}