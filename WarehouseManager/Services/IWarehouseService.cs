using Core.Models;

namespace WarehouseManager.Services;

public interface IWarehouseService
{
    Task<Product?> GetProductBySkuAsync(string? sku);
    Task<IEnumerable<Product>?> GetAllProductsAsync();
    Task<IEnumerable<string>> GetProductHistoryAsync(string? sku);
    Task CreateProductAsync(Product product);
    Task ReceiveProductAsync(string? sku, int quantity);
    Task ShipProductAsync(string? sku, int quantity);
    Task AdjustInventoryAsync(InventoryAdjustment inventoryAdjustment);
}
