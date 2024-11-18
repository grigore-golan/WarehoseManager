using Core.Commands;
using Core.Models;
using Core.Queries;
using Infrastructure.Mediator;

namespace WarehouseManager.Services;

public class WarehouseService(IMediator mediator) : IWarehouseService
{
    private readonly IMediator _mediator = mediator;

    public async Task<Product?> GetProductBySkuAsync(string? sku)
    {
        return (await _mediator.QueryAsync<GetProductBySkuQuery, GetProductBySkuQueryResult>(new GetProductBySkuQuery(sku)))?.Product;
    }

    public async Task<IEnumerable<Product>?> GetAllProductsAsync()
    {
        return (await _mediator.QueryAsync<GetAllProductsQuery, GetAllProductsQueryResult>(new GetAllProductsQuery()))?.Products;
    }

    public async Task<IEnumerable<string>> GetProductHistoryAsync(string? sku)
    {
        return (await _mediator.QueryAsync<GetProductHistoryQuery, GetProductHistoryQueryResult>(new GetProductHistoryQuery(sku)))?.ProductHistory.History ?? [];
    }

    public async Task CreateProductAsync(Product product)
    {
        await _mediator.ExecuteCommandAsync(new CreateProductCommand(product.Sku, product.Name, product.Quantity));
    }

    public async Task ReceiveProductAsync(string? sku, int quantity)
    {
        await _mediator.ExecuteCommandAsync(new ReceiveProductCommand(sku, quantity));
    }

    public async Task ShipProductAsync(string? sku, int quantity)
    {
        await _mediator.ExecuteCommandAsync(new ShipProductCommand(sku, quantity));
    }

    public async Task AdjustInventoryAsync(InventoryAdjustment inventoryAdjustment)
    {
        await _mediator.ExecuteCommandAsync(new AdjustInventoryCommand(inventoryAdjustment.Sku, inventoryAdjustment.Quantity, inventoryAdjustment.Reason));
    }
}
