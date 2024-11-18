namespace Database.Models.Events;

public class ProductEventEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Sku { get; set; } = string.Empty;
    public string EventType { get; set; } = string.Empty;
    public string EventData { get; set; } = string.Empty;
    public DateTimeOffset TimeStamp { get; set; } = DateTimeOffset.Now;
}
