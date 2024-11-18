using Database.Contexts;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Database;

public class Migrator(EventsContext eventsContext, ProjectionsContext projectionsContext, ILogger<Migrator> logger) : IMigrator
{
    private readonly EventsContext _eventsContext = eventsContext;
    private readonly ProjectionsContext _projectionsContext = projectionsContext;
    private readonly ILogger<Migrator> _logger = logger;

    public void Migrate()
    {
        try
        {
            TryMigrate(_eventsContext);
            TryMigrate(_projectionsContext);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while migrating the database.");
            throw;
        }
    }

    private void TryMigrate(DbContext context) 
    { 
        var pendingMigrations = context.Database.GetPendingMigrations(); 
        
        if (pendingMigrations.Any()) 
        { 
            _logger.LogInformation($"Applying {pendingMigrations.Count()} pending migrations for {context.GetType().Name}..."); 
            context.Database.Migrate(); 
        } 
        else 
        { 
            _logger.LogInformation($"No pending migrations for {context.GetType().Name}."); 
        } 
    }
}
