using Infrastructure.EventSourcing;

namespace Core.Events;

internal class ProductEvent(string sku) : IEvent, IEventDescription
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Sku { get; set; } = sku;
    public string Name { get; set; } = string.Empty;
    public DateTimeOffset TimeStamp { get; set; } = DateTimeOffset.Now;

    public virtual string GetDescription()
    {
        return $"EventName: {GetType().Name}; TimeStamp: {TimeStamp:dd-MM-yyyy HH:mm:ss};";
    }
}