using System.Text.Json;

namespace Ecommerce.Kafka;

public class KafkaSerializer
{
    public static string Serialize<TMessage>(TMessage message) =>
        JsonSerializer.Serialize(message);

    public static TMessage Deserialize<TMessage>(string json) =>
        JsonSerializer.Deserialize<TMessage>(json) ?? throw new NullReferenceException(nameof(TMessage));
}