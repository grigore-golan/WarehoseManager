namespace Infrastructure.Mediator;

public interface IMediator
{
    Task ExecuteCommandAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : ICommand;

    Task<TQueryResult?> QueryAsync<TQuery, TQueryResult>(TQuery query, CancellationToken cancellationToken = default)
        where TQuery : IQuery where TQueryResult : class, IQueryResult;
}