using Microsoft.EntityFrameworkCore;

namespace MigrationsExample;

public sealed class DungeonDbContext : DbContext
{
    public DbSet<DungeonRun> DungeonRuns { get; private init; } = null!;
    public DbSet<Dungeon> Dungeons { get; private init; } = null!;
    public DbSet<Hero> Heroes { get; private init; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Data Source=./data/Dungeon.db";
        optionsBuilder.UseSqlite(connectionString)
            .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
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