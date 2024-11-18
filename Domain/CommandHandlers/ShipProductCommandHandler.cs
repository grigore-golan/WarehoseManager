using Core.Commands;
using Core.Events;
using Infrastructure.EventSourcing;
using Infrastructure.Mediator;

namespace Core.CommandHandlers;

internal class ShipProductCommandHandler(IEventBus eventBus, IEventStore<ProductEvent> eventStore) : ICommandHandler<ShipProductCommand>
{
    private readonly IEventBus _eventBus = eventBus;
    private readonly IEventStore<ProductEvent> _eventStore = eventStore;

    public async Task HandleAsync(ShipProductCommand command, CancellationToken cancellationToken = default)
    {
        var @event = new ProductShippedEvent(command.Sku, command.Quantity);

        await _eventStore.SaveAsync(@event);
        await _eventBus.PublishAsync(@event);
    }
}
