using DungeonWalker.DataLayer.Model;

namespace DungeonWalker.DataLayer;

public interface IDungeonRepository
{
    Task SeedDatabaseAsync();

    Task<Dungeon?> GetDungeonAsync(int id);

    Task<IReadOnlyCollection<DungeonRun>> QueryRunsAsync(DungeonRunQueryParameters parameters);
}