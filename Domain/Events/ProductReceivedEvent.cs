namespace Core.Events;

internal class ProductReceivedEvent(string sku, int receivedQuantity) : ProductEvent(sku)
{
    public int ReceivedQuantity { get; set; } = receivedQuantity;

    public override string GetDescription()
    {
        return $"{base.GetDescription()} ReceivedQuantity: {ReceivedQuantity};";
    }
}
