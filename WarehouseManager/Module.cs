using Infrastructure.DependencyInjection;
using WarehouseManager.Services;

namespace WarehouseManager;

public class Module : DependencyModule
{
    public override void Load(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IWarehouseService, WarehouseService>();
    }
}
