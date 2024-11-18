using Core.Models;
using Core.Queries;
using Database.Contexts;
using Infrastructure;
using Infrastructure.Mediator;

namespace Core.QueryHandlers;

internal class GetProductBySkuQueryHandler(ProjectionsContext projectionsContext) : IQueryHandler<GetProductBySkuQuery, GetProductBySkuQueryResult>
{
    private readonly ProjectionsContext _projectionsContext = projectionsContext;

    public async Task<GetProductBySkuQueryResult?> HandleAsync(GetProductBySkuQuery query, CancellationToken cancellationToken = default)
    {
        var product = (await _projectionsContext.ProductProjections.FindAsync([query.Sku], cancellationToken: cancellationToken)).MapTo<Product>();
        if (product is null)
            return null;

        return new(product);
    }
}
