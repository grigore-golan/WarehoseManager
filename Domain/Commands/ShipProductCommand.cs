using Infrastructure.Mediator;

namespace Core.Commands;

public class ShipProductCommand(string? sku, int quantity) : ICommand
{
    public string? Sku { get; set; } = sku;
    public int Quantity { get; set; } = quantity;
}
