using Core.Models;
using Infrastructure.EventSourcing;
using Infrastructure.Mediator;

namespace Core.Queries;

public class GetProductHistoryQuery(string? sku) : IQuery
{
    public string? Sku { get; set; } = sku;
}

public class GetProductHistoryQueryResult(IEnumerable<IEventDescription> eventDescriptions) : IQueryResult
{
    public ProductHistory ProductHistory { get; set; } = new(eventDescriptions);
}
