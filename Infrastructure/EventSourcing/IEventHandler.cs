namespace Infrastructure.EventSourcing;

public interface IEventHandler<in T> where T : IEvent
{
    Task HandleAsync(T @event);
}
