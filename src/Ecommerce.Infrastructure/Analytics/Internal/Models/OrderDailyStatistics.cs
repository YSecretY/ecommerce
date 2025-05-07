using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ecommerce.Infrastructure.Analytics.Internal.Models;

internal class OrderDailyStatistics
{
    [BsonIgnore]
    public const string CollectionName = "OrderDailyStatistics";

    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; } = Guid.NewGuid();

    [BsonRepresentation(BsonType.String)]
    public DateOnly Date { get; set; }

    public int OrdersCount { get; set; }
}