using CSharpFunctionalExtensions;
using DungeonWalker.DataLayer.Model;

namespace DungeonWalker.DataLayer;

public interface IDungeonRepository
{
    Task SeedDatabaseAsync();

    Task<Dungeon?> GetDungeonAsync(int id);

    Task<IReadOnlyCollection<DungeonRun>> QueryRunsAsync(DungeonRunQueryParameters parameters);

    Task<Result<int, ApiError>> CreateDungeonRunAsync(DungeonRunPost dungeonRun);

    Task<DungeonRun?> GetDungeonRunAsync(int id);
}