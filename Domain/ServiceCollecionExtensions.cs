using FluentValidation;
using Infrastructure.Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class ServiceCollectionExtensions
{
    public static void AddValidator<TCommand, TValidator>(this IServiceCollection services)
        where TValidator : class, IValidator<TCommand>
        where TCommand : ICommand
    {
        services.AddTransient<IValidator<TCommand>, TValidator>();
    }

    public static void AddCommandHandler<TCommand, THandler>(this IServiceCollection services)
        where THandler : class, ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        services.AddScoped<ICommandHandler<TCommand>, THandler>();
    }

    public static void AddQueryHandler<TQuery, TResult, THandler>(this IServiceCollection services)
        where TQuery : IQuery
        where TResult : IQueryResult
        where THandler : class, IQueryHandler<TQuery, TResult>
    {
        services.AddScoped<IQueryHandler<TQuery, TResult>, THandler>();
    }
}