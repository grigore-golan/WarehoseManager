using Core.Commands;
using Core.Events;
using Infrastructure.EventSourcing;
using Infrastructure.Mediator;

namespace Core.CommandHandlers;

internal class AdjustInventoryCommandHandler(IEventBus eventBus, IEventStore<ProductEvent> eventStore) : ICommandHandler<AdjustInventoryCommand>
{
    private readonly IEventBus _eventBus = eventBus;
    private readonly IEventStore<ProductEvent> _eventStore = eventStore;

    public async Task HandleAsync(AdjustInventoryCommand command, CancellationToken cancellationToken = default)
    {
        var @event = new InventoryAdjustedEvent(command.Sku, command.Quantity, command.Reason);

        await _eventStore.SaveAsync(@event);
        await _eventBus.PublishAsync(@event);
    }
}
