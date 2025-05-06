namespace Ecommerce.Infrastructure.Mongo.Internal.Services;

internal interface IEventsInfoService
{
    public Task<bool> IsEventAlreadyProcessedAsync(Guid eventId);

    public Task MarkHandledAsync(Guid eventId);
}