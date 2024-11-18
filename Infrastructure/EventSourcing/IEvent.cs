namespace Infrastructure.EventSourcing;

public interface IEvent
{
    public Guid Id { get; set; }
    public DateTimeOffset TimeStamp { get; set; }
}
