using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ecommerce.Infrastructure.Analytics.Models;

internal class ProductDailyStatistics
{
    [BsonIgnore]
    public const string CollectionName = "ProductDailyStatistics";

    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; private set; } = Guid.NewGuid();

    [BsonRepresentation(BsonType.String)]
    public Guid ProductId { get; private set; }

    [BsonRepresentation(BsonType.String)]
    public DateOnly Date { get; private set; }

    public int SoldCount { get; private set; }

    public int ViewsCount { get; private set; }
}