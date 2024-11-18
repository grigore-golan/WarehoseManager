namespace Infrastructure.EventSourcing;

public interface IEventStore<T> where T : IEvent
{
    Task SaveAsync(T @event);
    Task<IEnumerable<T>> GetAsync(string Sku);
}