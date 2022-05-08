using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;

namespace MigrationsExample;

public sealed class DungeonDbContext : DbContext
{
    public DbSet<DungeonRun> DungeonRuns { get; private init; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite("Data Source=../data/Dungeon.db");
    }

    protected override void OnModelCreating(ModelBuilder model)
    {
        var dungeonRun = model.Entity<DungeonRun>();

        dungeonRun.HasKey(d => d.Id);
        dungeonRun.ToTable("DungeonRun");
    }
}