namespace Infrastructure.EventSourcing;

public interface IEventBus
{
    Task PublishAsync(IEvent @event);
    void Subscribe<T, TH>() 
        where T : IEvent 
        where TH : IEventHandler<T>;
}
