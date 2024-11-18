using Core.Commands;
using Core.Events;
using Infrastructure.EventSourcing;
using Infrastructure.Mediator;

namespace Core.CommandHandlers;

internal class CreateProductCommandHandler(IEventBus eventBus, IEventStore<ProductEvent> eventStore) : ICommandHandler<CreateProductCommand>
{
    private readonly IEventBus _eventBus = eventBus;
    private readonly IEventStore<ProductEvent> _eventStore = eventStore;

    public async Task HandleAsync(CreateProductCommand command, CancellationToken cancellationToken = default)
    {
        var @event = new ProductReceivedEvent(command.Sku, command.Quantity) { Name = command.Name };

        await _eventStore.SaveAsync(@event);
        await _eventBus.PublishAsync(@event);
    }
}
