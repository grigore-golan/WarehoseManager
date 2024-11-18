using Infrastructure.Mediator;

namespace Core.Commands;

public class CreateProductCommand(string? sku, string? name, int quantity) : ICommand
{
    public string? Sku { get; set; } = sku;
    public string? Name { get; set; } = name;
    public int Quantity { get; set; } = quantity;
}
