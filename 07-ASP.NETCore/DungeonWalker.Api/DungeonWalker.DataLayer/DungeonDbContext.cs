using System.Reflection;
using DungeonWalker.DataLayer.Model;
using Microsoft.EntityFrameworkCore;

namespace DungeonWalker.DataLayer;

public sealed class DungeonDbContext : DbContext
{
    public DbSet<DungeonRun> DungeonRuns { get; private init; } = null!;
    public DbSet<Dungeon> Dungeons { get; private init; } = null!;
    public DbSet<Hero> Heroes { get; private init; } = null!;

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
        var dungeonRun = modelBuilder.Entity<DungeonRun>();
        var dungeon = modelBuilder.Entity<Dungeon>();
        var hero = modelBuilder.Entity<Hero>();

        dungeonRun.HasKey(d => d.Id);

        dungeonRun.HasOne(dr => dr.Dungeon)
            .WithMany(d => d.Runs);
        dungeonRun.HasOne(dr => dr.Hero)
            .WithMany(h => h.Runs);

        dungeonRun.ToTable("DungeonRun");

        dungeon.HasKey(d => d.Id);
        dungeon.ToTable("Dungeon");

        hero.HasKey(d => d.Id);
        hero.ToTable("Hero");
    }
}