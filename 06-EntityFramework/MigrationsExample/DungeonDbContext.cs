using Microsoft.EntityFrameworkCore;

namespace MigrationsExample;

public sealed class DungeonDbContext : DbContext
{
    public DbSet<DungeonRun> DungeonRuns { get; private init; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Use the password from https://gienieczko.com/teaching/csharp/6-entity-framework/0-database-setup.
        var connectionString = "server=sql11.freesqldatabase.com;user=sql11491290;password=<PASSWORD>;database=sql11491290";
        var version = ServerVersion.AutoDetect(connectionString);
        optionsBuilder.UseMySql(connectionString, version)
            .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var dungeonRun = modelBuilder.Entity<DungeonRun>();

        dungeonRun.HasKey(d => d.Id);
        dungeonRun.ToTable("DungeonRun");
    }
}