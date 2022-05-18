using DungeonWalker.Api.Model;
using DungeonWalker.DataLayer;
using DungeonWalker.DataLayer.Model;
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
    /// <response code="404">If the Dungeon with given ID does not exist.</response>
    [HttpGet]
    [Route("/dungeons/{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
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
    /// <returns>Task representing the asynchronous query operation.</returns>
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
    ///     Get a DungeonRun by ID.
    /// </summary>
    /// <param name="id">ID to query by.</param>
    /// <returns>
    ///     Task representing the asynchronous GET operation.
    /// </returns>
    /// <response code="404">If the DungeonRun with given ID does not exist.</response>
    [HttpGet]
    [Route("/runs/{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<DungeonRunView>> GetDungeonRunAsync(int id)
    {
        _logger.LogInformation("Fetching DungeonRun {Id}...", id);

        var run = await _repository.GetDungeonRunAsync(id);

        if (run is null)
        {
            _logger.LogInformation("DungeonRun {Id} not found.", id);
            return NotFound();
        }

        return run.ToView();
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
    [Route("seed")]
    public async Task SeedAsync()
    {
        _logger.LogInformation("Reseeding the database.");
        await _repository.SeedDatabaseAsync();
    }

    /// <summary>
    ///     Record a DungeonRun in the system.
    /// </summary>
    /// <param name="dungeonRun">Data of the Run.</param>
    /// <returns>
    ///     Task representing the asynchronous POST operation.
    /// </returns>
    /// <response code="400">If a Dungeon with the given name or used Hero class do not exist.</response>
    [HttpPost]
    [Route("runs")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> PostAsync([FromBody] DungeonRunPost dungeonRun)
    {
        var result = await _repository.CreateDungeonRunAsync(dungeonRun);

        if (result.IsSuccess)
        {
            return CreatedAtAction("GetDungeonRun", new { id = result.Value }, null);
        }
        else
        {
            return Problem(result.Error.Message, statusCode: (int)result.Error.Code);
        }
    }
}
