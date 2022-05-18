using DungeonWalker.Api.Model;
using DungeonWalker.DataLayer;
using Microsoft.AspNetCore.Mvc;

namespace DungeonWalker.Api.Controllers;

/// <summary>
///     Controller for Dungeons and DungeonRuns.
/// </summary>
[ApiController]
[Route("[controller]")]
public class DungeonController : ControllerBase
{
    private readonly IDungeonRepository _repository;

    private readonly ILogger<DungeonController> _logger;

    /// <summary>
    ///     Creates a new instance of <see cref="DungeonController"/>.
    /// </summary>
    /// <param name="repository">Instance of <see cref="IDungeonRepository"/> to use by the controller.</param>
    /// <param name="logger">Instance of <see cref="ILogger{DungeonController}"/> to use by the controller.</param>
    public DungeonController(IDungeonRepository repository, ILogger<DungeonController> logger) =>
        (_repository, _logger) = (repository, logger);

    /// <summary>
    ///     Get a Dungeon by ID.
    /// </summary>
    /// <param name="id">ID to query by.</param>
    /// <returns>
    ///     Task representing the asynchronous GET operation.
    /// </returns>
    [HttpGet]
    [Route("/dungeons/{id}")]
    public async Task<ActionResult<DungeonView>> GetDungeonAsync(int id)
    {
        _logger.LogInformation("Fetching Dungeon {Id}...", id);

        var dungeon = await _repository.GetDungeonAsync(id);

        if (dungeon is null)
        {
            _logger.LogInformation("Dungeon {Id} not found.", id);
            return NotFound();
        }

        return dungeon.ToView();
    }

    /// <summary>
    ///     Query a DungeonRun, filtering by name and/or hero class.
    /// </summary>
    /// <param name="dungeonName">
    ///     Return only DungeonRuns of this Dungeon.
    /// </param>
    /// <param name="heroClass">
    ///     Return only DungeonRuns of this Hero class.
    /// </param>
    /// <returns></returns>
    [HttpGet]
    [Route("/runs")]
    public async Task<ActionResult<IEnumerable<DungeonRunView>>> QueryRunsAsync(
        [FromQuery] string? dungeonName,
        [FromQuery] string? heroClass
    )
    {
        _logger.LogInformation("Querying runs of {HeroClass} in {DungeonName}", heroClass, dungeonName);

        var dungeonRuns = await _repository.QueryRunsAsync(new(dungeonName, heroClass));

        return dungeonRuns.Select(d => d.ToView()).ToList();
    }

    /// <summary>
    ///     Seed the database with example data.
    /// </summary>
    /// <remarks>
    ///     The database gets completely cleared and recreated.
    /// </remarks>
    /// <returns>
    ///     Task representing the asynchronous seed operation.
    /// </returns>
    [HttpPost]
    [Route("[action]")]
    public async Task SeedAsync()
    {
        _logger.LogInformation("Reseeding the database.");
        await _repository.SeedDatabaseAsync();
    }
}
