using Core.CommandHandlers;
using Core.Commands;
using Core.EventHandlers;
using Core.Events;
using Core.Queries;
using Core.QueryHandlers;
using Core.Validation;
using Infrastructure.DependencyInjection;
using Infrastructure.EventSourcing;
using Infrastructure.Mediator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core;

public class Module : DependencyModule
{
    public override void Load(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IEventBus, InMemoryEventBus>();
        services.AddSingleton<IMediator, Mediator>();

        services.AddScoped<IEventStore<ProductEvent>, ProductEventStore>();
        services.AddScoped<IEventHandlerSubscriber, EventHandlerSubscriber>();

        // Add Event Handlers
        services.AddScoped<IEventHandler<InventoryAdjustedEvent>, InventoryAdjustedEventHandler>();
        services.AddScoped<IEventHandler<ProductReceivedEvent>, ProductReceivedEventHandler>();
        services.AddScoped<IEventHandler<ProductShippedEvent>, ProductShippedEventHandler>();

        // Add Command Handlers
        services.AddCommandHandler<AdjustInventoryCommand, AdjustInventoryCommandHandler>();
        services.AddCommandHandler<CreateProductCommand, CreateProductCommandHandler>();
        services.AddCommandHandler<ReceiveProductCommand, ReceiveProductCommandHandler>();
        services.AddCommandHandler<ShipProductCommand, ShipProductCommandHandler>();

        // Add Command Validators
        services.AddValidator<AdjustInventoryCommand, AdjustInventoryCommandValidator>();
        services.AddValidator<CreateProductCommand, CreateProductCommandValidator>();
        services.AddValidator<ReceiveProductCommand, ReceiveProductCommandValidator>();
        services.AddValidator<ShipProductCommand, ShipProductCommandValidator>();

        // Add Query Handlers
        services.AddQueryHandler<GetAllProductsQuery, GetAllProductsQueryResult, GetAllProductsQueryHandler>();
        services.AddQueryHandler<GetProductBySkuQuery, GetProductBySkuQueryResult, GetProductBySkuQueryHandler>();
        services.AddQueryHandler<GetProductHistoryQuery, GetProductHistoryQueryResult, GetProductHistoryQueryHandler>();
    }
}
