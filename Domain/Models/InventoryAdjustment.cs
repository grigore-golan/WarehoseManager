namespace Core.Models;

public class InventoryAdjustment
{
    public string? Sku { get; set; }
    public int Quantity { get; set; }
    public string? Reason { get; set; }
}
