using Ecommerce.Core.Abstractions.Time;
using Ecommerce.Infrastructure.Analytics.Internal.Mongo.Models;
using Ecommerce.Infrastructure.Mongo.Internal;
using MongoDB.Driver;

namespace Ecommerce.Infrastructure.Events.Internal.Services;

internal class EventsInfoService(
    MongoDbContext dbContext,
    IDateTimeProvider dateTimeProvider
) : IEventsInfoService
{
    private readonly IMongoCollection<ProcessedEvent> _collection =
        dbContext.Database.GetCollection<ProcessedEvent>(ProcessedEvent.CollectionName);

    public async Task<bool> IsEventAlreadyProcessedAsync(Guid eventId) =>
        await _collection.Find(e => e.EventId == eventId).AnyAsync();

    public async Task MarkProcessedAsync(Guid eventId) =>
        await _collection.InsertOneAsync(new ProcessedEvent
        {
            EventId = eventId,
            ProcessedAtUtc = dateTimeProvider.UtcNow
        });
}