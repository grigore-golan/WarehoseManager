using Infrastructure.EventSourcing;
using Microsoft.Extensions.DependencyInjection;

namespace Core;

internal class InMemoryEventBus(IServiceProvider serviceProvider) : IEventBus
{
    private readonly Dictionary<string, List<Type>> _handlers = [];
    private readonly List<Type> _eventTypes = [];
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task PublishAsync(IEvent @event)
    {
        var eventName = @event.GetType().Name;
        if (!_handlers.ContainsKey(eventName)) return;

        foreach (var handlerType in _handlers[eventName])
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var handler = scope.ServiceProvider.GetService(handlerType);
                var eventType = _eventTypes.SingleOrDefault(t => t.Name == eventName);
                var concreteType = typeof(IEventHandler<>).MakeGenericType(eventType);

                var method = concreteType.GetMethod(nameof(IEventHandler<IEvent>.HandleAsync));
                if (method != null && handler != null)
                {
                    await (Task)method.Invoke(handler, [@event])!;
                }
            }
        }
    }

    public void Subscribe<T, TH>()
        where T : IEvent
        where TH : IEventHandler<T>
    {
        var eventName = typeof(T).Name;
        var handlerType = typeof(TH);

        if (!_handlers.TryGetValue(eventName, out List<Type>? eventHandlers))
        {
            eventHandlers = ([]);
            _handlers.Add(eventName, eventHandlers);
        }
        if (eventHandlers.Any(s => s.GetType() == handlerType))
        {
            throw new ArgumentException($"Handler Type {handlerType.Name} already registered for '{eventName}'", nameof(handlerType));
        }

        eventHandlers.Add(handlerType);
        _eventTypes.Add(typeof(T));
    }
}
