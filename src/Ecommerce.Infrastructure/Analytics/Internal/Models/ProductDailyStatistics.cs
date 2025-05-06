using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ecommerce.Infrastructure.Analytics.Internal.Models;

internal class ProductDailyStatistics
{
    [BsonIgnore]
    public const string CollectionName = "ProductDailyStatistics";

    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; } = Guid.NewGuid();

    [BsonRepresentation(BsonType.String)]
    public Guid ProductId { get; set; }

    [BsonRepresentation(BsonType.String)]
    public DateOnly Date { get; set; }

    public int SoldCount { get; set; }

    public int ViewsCount { get; set; }
}