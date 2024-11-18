using Infrastructure.Database;
using Infrastructure.DependencyInjection;
using Infrastructure.EventSourcing;
using MudBlazor.Services;
using WarehouseManager;
using WarehouseManager.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add Mud services
builder.Services.AddMudServices();

builder.Services.LoadDependencyModules(
    builder.Configuration,
    typeof(Core.Module).Assembly,
    typeof(Database.Module).Assembly,
    typeof(Module).Assembly);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var migrator = scope.ServiceProvider.GetRequiredService<IMigrator>();
    migrator.Migrate();

    var eventHandlerSubscriber = scope.ServiceProvider.GetRequiredService<IEventHandlerSubscriber>();
    eventHandlerSubscriber.SubscribeEventHandlers();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
