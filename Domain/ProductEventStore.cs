using Core.Events;
using Database.Contexts;
using Database.Models.Events;
using Infrastructure.EventSourcing;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Core;

internal class ProductEventStore(EventsContext dbContext) : IEventStore<ProductEvent>
{
    private readonly EventsContext _dbContext = dbContext;

    public async Task SaveAsync(ProductEvent @event)
    {
        await _dbContext.ProductEventStore.AddAsync(SerializeEventEntity(@event));
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<ProductEvent>> GetAsync(string sku)
    {
        var rawEvents = await _dbContext.ProductEventStore
            .Where(e => e.Sku == sku)
            .OrderBy(x => x.TimeStamp)
            .ToListAsync();

        List<ProductEvent> events = [];
        foreach (var rawEvent in rawEvents)
        {
            var deserializedEvent = DeserializeEventEntity(rawEvent);
            events.Add(deserializedEvent);
        }

        return events;
    }

    private static ProductEventEntity SerializeEventEntity(ProductEvent @event)
    {
        var serializedEvent = new ProductEventEntity
        {
            Sku = @event.Sku,
            EventType = @event.GetType().Name!,
            EventData = JsonSerializer.Serialize(@event, Type.GetType($"{typeof(ProductEvent).Namespace}.{@event.GetType().Name}")!)
        };
        return serializedEvent;
    }

    private static ProductEvent DeserializeEventEntity(ProductEventEntity @event)
    {
        var eventType = Type.GetType($"{typeof(ProductEvent).Namespace}.{@event.EventType}");
        if (eventType != null)
        {
            return (ProductEvent)JsonSerializer.Deserialize(@event.EventData, eventType)!;
        }
        throw new InvalidOperationException("Unknown event type");
    }
}
