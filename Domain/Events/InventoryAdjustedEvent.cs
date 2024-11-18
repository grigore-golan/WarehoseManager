namespace Core.Events;

internal class InventoryAdjustedEvent(string sku, int newQuantity, string reason) : ProductEvent(sku)
{
    public int NewQuantity { get; set; } = newQuantity;
    public string Reason { get; set; } = reason;

    public override string GetDescription()
    {
        return $"{base.GetDescription()} NewQuantity: {NewQuantity}; Reason: {Reason};";
    }
}
