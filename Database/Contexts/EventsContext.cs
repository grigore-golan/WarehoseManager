using Database.Models.Events;
using Microsoft.EntityFrameworkCore;

namespace Database.Contexts;

public class EventsContext(DbContextOptions<EventsContext> options) : DbContext(options)
{
    public DbSet<ProductEventEntity> ProductEventStore { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
