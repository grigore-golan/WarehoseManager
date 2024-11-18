using Core.Events;
using Infrastructure.EventSourcing;

namespace Core;

public class EventHandlerSubscriber(IEventBus eventBus) : IEventHandlerSubscriber
{
    private readonly IEventBus _eventBus = eventBus;

    public void SubscribeEventHandlers()
    {
        _eventBus.Subscribe<ProductReceivedEvent, IEventHandler<ProductReceivedEvent>>();
        _eventBus.Subscribe<ProductShippedEvent, IEventHandler<ProductShippedEvent>>();
        _eventBus.Subscribe<InventoryAdjustedEvent, IEventHandler<InventoryAdjustedEvent>>();
    }
}
