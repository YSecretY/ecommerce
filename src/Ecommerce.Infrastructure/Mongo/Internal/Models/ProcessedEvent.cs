using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ecommerce.Infrastructure.Mongo.Internal.Models;

public class ProcessedEvent
{
    [BsonIgnore]
    public const string CollectionName = "ProcessedEvents";

    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid EventId { get; set; }

    [BsonRepresentation(BsonType.String)]
    public DateTime ProcessedAtUtc { get; set; }
}