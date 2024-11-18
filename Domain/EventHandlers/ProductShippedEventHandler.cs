using Core.Events;
using Database.Contexts;
using Infrastructure.EventSourcing;

namespace Core.EventHandlers;

internal class ProductShippedEventHandler(ProjectionsContext projectionsContext) : IEventHandler<ProductShippedEvent>
{
    private readonly ProjectionsContext _projectionsContext = projectionsContext;

    public async Task HandleAsync(ProductShippedEvent @event)
    {
        var product = await _projectionsContext.ProductProjections.FindAsync(@event.Sku)
            ?? throw new Exception("Product not found");

        if (product.Quantity > @event.ShippedQuantity)
        {
            product.Quantity -= @event.ShippedQuantity;
        }
        else
        {
            throw new Exception("Not enough quantity to ship");
        }

        await _projectionsContext.SaveChangesAsync();
    }
}
