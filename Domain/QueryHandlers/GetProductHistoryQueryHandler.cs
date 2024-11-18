using Core.Events;
using Core.Queries;
using Infrastructure.EventSourcing;
using Infrastructure.Mediator;

namespace Core.QueryHandlers;

internal class GetProductHistoryQueryHandler(IEventStore<ProductEvent> eventStore) : IQueryHandler<GetProductHistoryQuery, GetProductHistoryQueryResult>
{
    private readonly IEventStore<ProductEvent> _eventStore = eventStore;

    public async Task<GetProductHistoryQueryResult?> HandleAsync(GetProductHistoryQuery query, CancellationToken cancellationToken = default)
    {
        var events = await _eventStore.GetAsync(query.Sku ?? string.Empty);

        if (events is null || !events.Any())
            return null;

        return new GetProductHistoryQueryResult(events);
    }
}
