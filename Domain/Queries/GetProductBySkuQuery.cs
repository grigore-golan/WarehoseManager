using Core.Models;
using Infrastructure.Mediator;

namespace Core.Queries;

public class GetProductBySkuQuery(string? sku) : IQuery
{
    public string? Sku { get; set; } = sku;
}

public class GetProductBySkuQueryResult(Product product) : IQueryResult
{
    public Product Product { get; set; } = product;
}
