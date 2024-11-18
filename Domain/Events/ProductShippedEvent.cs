namespace Core.Events;

internal class ProductShippedEvent(string sku, int shippedQuantity) : ProductEvent(sku)
{
    public int ShippedQuantity { get; set; } = shippedQuantity;

    public override string GetDescription()
    {
        return $"{base.GetDescription()} ShippedQuantity: {ShippedQuantity};";
    }
}
