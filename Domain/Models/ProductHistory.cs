using Infrastructure.EventSourcing;

namespace Core.Models;

public class ProductHistory(IEnumerable<IEventDescription> eventDescriptions)
{
    public IEnumerable<string> History { get; set; } = eventDescriptions.Select(eventDescription => eventDescription.GetDescription());
}
