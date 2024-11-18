using Core.Events;
using Database.Contexts;
using Database.Models.Projections;
using Infrastructure.EventSourcing;

namespace Core.EventHandlers;

internal class ProductReceivedEventHandler(ProjectionsContext projectionsContext) : IEventHandler<ProductReceivedEvent>
{
    private readonly ProjectionsContext _projectionsContext = projectionsContext;

    public async Task HandleAsync(ProductReceivedEvent @event)
    {
        var product = await _projectionsContext.ProductProjections.FindAsync(@event.Sku);

        if (product == null)
        {
            product = new ProductProjectionEntity
            {
                Sku = @event.Sku,
                Name = @event.Name,
                Quantity = @event.ReceivedQuantity
            };

            _projectionsContext.ProductProjections.Add(product);
        }
        else
        {
            product.Quantity += @event.ReceivedQuantity;
        }

        await _projectionsContext.SaveChangesAsync();
    }
}
