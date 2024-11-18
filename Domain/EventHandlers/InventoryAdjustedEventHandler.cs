using Core.Events;
using Database.Contexts;
using Infrastructure.EventSourcing;

namespace Core.EventHandlers;

internal class InventoryAdjustedEventHandler(ProjectionsContext projectionsContext) : IEventHandler<InventoryAdjustedEvent>
{
    private readonly ProjectionsContext _projectionsContext = projectionsContext;

    public async Task HandleAsync(InventoryAdjustedEvent @event)
    {
        var product = await _projectionsContext.ProductProjections.FindAsync(@event.Sku)
            ?? throw new Exception("Product not found");

        product.Quantity = @event.NewQuantity;

        await _projectionsContext.SaveChangesAsync();
    }
}
