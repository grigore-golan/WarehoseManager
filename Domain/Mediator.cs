using FluentValidation;
using Infrastructure.Mediator;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Core;

public class Mediator(IServiceProvider serviceProvider, ILogger<Mediator> logger) : IMediator
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly ILogger<Mediator> _logger = logger;

    public async Task ExecuteCommandAsync<TCommand>(TCommand command, CancellationToken cancellationToken) where TCommand : ICommand
    {
        if (!Validate(command))
            return;

        using (var scope = _serviceProvider.CreateScope())
        {
            var commandHandler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();
            await commandHandler.HandleAsync(command, cancellationToken);
        }
    }

    public async Task<TResult?> QueryAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken) where TQuery : IQuery where TResult : class, IQueryResult
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var queryHandler = scope.ServiceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
            return await queryHandler.HandleAsync(query, cancellationToken);
        }
    }

    private bool Validate<TCommand>(TCommand command) where TCommand : ICommand
    {
        var validator = _serviceProvider.GetRequiredService<IValidator<TCommand>>();

        var validationResult = validator.Validate(command);
        if (validationResult.Errors.Count > 0)
            foreach (var error in validationResult.Errors)
                _logger.LogWarning($"ValidationError: {error.ErrorMessage}");

        return validationResult.IsValid;
    }
}