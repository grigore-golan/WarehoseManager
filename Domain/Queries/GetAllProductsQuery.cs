using Core.Models;
using Infrastructure.Mediator;

namespace Core.Queries;

public class GetAllProductsQuery : IQuery
{
}

public class GetAllProductsQueryResult(ICollection<Product> products) : IQueryResult
{
    public ICollection<Product> Products { get; set; } = products;
}