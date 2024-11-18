using Infrastructure.Mediator;

namespace Core.Commands;

public class AdjustInventoryCommand(string? sku, int quantity, string? reason) : ICommand
{
    public string? Sku { get; set; } = sku;
    public int Quantity { get; set; } = quantity;
    public string? Reason { get; set; } = reason;
}