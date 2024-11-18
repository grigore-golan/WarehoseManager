using Database.Contexts;
using Infrastructure.Database;
using Infrastructure.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Database;

public class Module : DependencyModule
{
    public override void Load(IServiceCollection services, IConfiguration configuration)
    {
        string eventsConnectionString = string.Empty;
        string projectionsConnectionString = string.Empty;

        eventsConnectionString = configuration.GetConnectionString("EventStore") ?? throw new Exception("Connection string could not be configured as null.");
        projectionsConnectionString = configuration.GetConnectionString("ProjectionStore") ?? throw new Exception("Connection string could not be configured as null.");

        services.AddDbContext<EventsContext>(options =>
        {
            options.UseSqlServer(eventsConnectionString);
        });
        services.AddDbContext<ProjectionsContext>(options =>
        {
            options.UseSqlServer(projectionsConnectionString);
        });

        services.AddScoped<IMigrator, Migrator>();
    }
}
