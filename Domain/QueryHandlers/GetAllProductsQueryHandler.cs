using Core.Models;
using Core.Queries;
using Database.Contexts;
using Infrastructure;
using Infrastructure.Mediator;
using Microsoft.EntityFrameworkCore;

namespace Core.QueryHandlers;

internal class GetAllProductsQueryHandler(ProjectionsContext projectionsContext) : IQueryHandler<GetAllProductsQuery, GetAllProductsQueryResult>
{
    private readonly ProjectionsContext _projectionsContext = projectionsContext;

    public async Task<GetAllProductsQueryResult?> HandleAsync(GetAllProductsQuery query, CancellationToken cancellationToken = default)
    {
        var products = (await _projectionsContext.ProductProjections.ToListAsync(cancellationToken: cancellationToken)).MapTo<List<Product>>();
        if (products is null)
            return null;

        return new(products);
    }
}
