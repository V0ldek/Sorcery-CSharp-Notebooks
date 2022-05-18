using Adder.DataLayer;
using Microsoft.AspNetCore.Mvc;

namespace Adder.Api.Controllers;

/// <summary>
///   The Adder controller.
/// </summary>
[ApiController]
[Route("[controller]")]
public class AdderController : ControllerBase
{
    private readonly INumberRepository _repository;

    /// <summary>
    ///     Create a new instance of <see cref="AdderController"/>.
    /// </summary>
    /// <param name="repository"><see cref="INumberRepository"/> instance to use.</param>
    public AdderController(INumberRepository repository) => _repository = repository;

    /// <summary>
    ///     Gets the sum of "two" and "three".
    /// </summary>
    /// <returns>
    ///     The sum of "two" and "three".
    /// </returns>
    /// <response code="400">If the item is null</response>
    [HttpGet]
    [Route("sum")]
    public async Task<int> GetSumAsync()
    {
        var x = await _repository.GetNumberAsync("two");
        var y = await _repository.GetNumberAsync("three");

        return (x ?? 0) + (y ?? 0);
    }

    /// <summary>
    ///     Seed the database with example data.
    /// </summary>
    /// <returns>
    ///     Task representing the asynchronous seed operation.
    /// </returns>
    [HttpPost]
    [Route("seed")]
    public async Task SeedAsync()
    {
        await _repository.SeedAsync();
    }
}
