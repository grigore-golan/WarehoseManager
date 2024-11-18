using Database.Models.Projections;
using Microsoft.EntityFrameworkCore;

namespace Database.Contexts;

public class ProjectionsContext(DbContextOptions<ProjectionsContext> options) : DbContext(options)
{
    public DbSet<ProductProjectionEntity> ProductProjections { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductProjectionEntity>().HasKey(x => x.Sku);

        base.OnModelCreating(modelBuilder);
    }
}
