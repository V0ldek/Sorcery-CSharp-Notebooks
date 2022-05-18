using DungeonWalker.DataLayer.Model;
using Microsoft.EntityFrameworkCore;

namespace DungeonWalker.DataLayer;

public sealed class DungeonRepository : IDungeonRepository
{
    private readonly DungeonDbContext _dbContext;

    public DungeonRepository(DungeonDbContext dbContext) => _dbContext = dbContext;

    public Task<Dungeon?> GetDungeonAsync(int id) => _dbContext.Dungeons.SingleOrDefaultAsync(d => d.Id == id);

    public async Task<IReadOnlyCollection<DungeonRun>> QueryRunsAsync(DungeonRunQueryParameters parameters)
    {
        ArgumentNullException.ThrowIfNull(parameters);

        IQueryable<DungeonRun> result = _dbContext.DungeonRuns
            .Include(r => r.Dungeon)
            .Include(r => r.Hero);

        if (parameters.DungeonName is not null)
        {
            result = result.Where(r => r.Dungeon.Name == parameters.DungeonName);
        }

        if (parameters.HeroClass is not null)
        {
            result = result.Where(r => r.Hero.Name == parameters.HeroClass);
        }

        return await result.ToListAsync();
    }

    public async Task SeedDatabaseAsync()
    {
        await _dbContext.Database.EnsureDeletedAsync();
        await _dbContext.Database.EnsureCreatedAsync();

        var warrior = new Hero("Warrior");
        var rogue = new Hero("Rogue");
        var wizard = new Hero("Wizard");

        var magicalMaze = new Dungeon("Magical Maze", 16);
        var adventure = new Dungeon("Adventure", 16);

        _dbContext.DungeonRuns.Add(new(magicalMaze, warrior, 16, 180));
        _dbContext.DungeonRuns.Add(new(magicalMaze, rogue, 14, 130));
        _dbContext.DungeonRuns.Add(new(magicalMaze, wizard, 15, 150));
        _dbContext.DungeonRuns.Add(new(adventure, warrior, 10, 120));
        _dbContext.DungeonRuns.Add(new(adventure, rogue, 7, 69));
        _dbContext.DungeonRuns.Add(new(adventure, wizard, 7, 73));

        await _dbContext.SaveChangesAsync();
    }
}