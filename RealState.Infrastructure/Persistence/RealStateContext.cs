using Microsoft.EntityFrameworkCore;
using RealState.Domain.Entities;

namespace RealState.Infrastructure.Persistence;

public class RealStateContext : DbContext
{
    public RealStateContext()
    {
        
    }
    public RealStateContext(DbContextOptions<RealStateContext> options) : base(options)
    {
    }

    public DbSet<Owner> Owners { get; set; }
    public DbSet<Property> Properties { get; set; }
    public DbSet<PropertyImage> PropertyImages { get; set; }
    public DbSet<PropertyTrace> PropertyTraces { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configurar tipo de columna directamente
        modelBuilder.Entity<Property>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<PropertyTrace>()
            .Property(pt => pt.Value)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<PropertyTrace>()
            .Property(pt => pt.Tax)
            .HasColumnType("decimal(18,2)");
    }
}
