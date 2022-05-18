using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Adder.DataLayer;

public sealed class AdderDbContext : DbContext
{
    public DbSet<Number> Numbers { get; private init; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var assemblyPath = Assembly.GetExecutingAssembly().Location;
        var assemblyDirectory = Path.GetDirectoryName(assemblyPath);
        var path = Path.Combine(assemblyDirectory!, "Adder.db");
        var connectionString = $"Data Source={path}";
        optionsBuilder.UseSqlite(connectionString)
            .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Warning)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var dungeonRun = modelBuilder.Entity<Number>();

        dungeonRun.HasKey(n => n.Id);
        dungeonRun.HasIndex(n => n.Name).IsUnique();
        dungeonRun.ToTable("Number");
    }
}
