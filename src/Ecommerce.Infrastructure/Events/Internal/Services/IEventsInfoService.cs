namespace Ecommerce.Infrastructure.Events.Internal.Services;

internal interface IEventsInfoService
{
    public Task<bool> IsEventAlreadyProcessedAsync(Guid eventId);

    public Task MarkProcessedAsync(Guid eventId);
}