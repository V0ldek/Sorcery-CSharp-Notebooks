using Adder.Api.Controllers;
using Adder.DataLayer;

namespace Adder.Api.UnitTests;

public class AdderControllerUnitTests
{
    [Fact]
    public async Task Sum_WhenBothNumbersExistInRepository_ReturnsSum()
    {
        var repository = new FixedRepository(("two", 2), ("three", 3));
        var sut = new AdderController(repository);

        var result = await sut.GetSumAsync();

        Assert.Equal(5, result);
    }

    private sealed class FixedRepository : INumberRepository
    {
        private readonly IDictionary<string, int> _numbers = new Dictionary<string, int>();

        public FixedRepository(params (string, int)[] values) =>
            _numbers = values.ToDictionary(v => v.Item1, v => v.Item2);

        public Task<int?> GetNumberAsync(string name)
        {
            if (_numbers.TryGetValue(name, out var value))
            {
                return Task.FromResult<int?>(value);
            }
            return Task.FromResult<int?>(null);
        }

        public Task SeedAsync() => Task.CompletedTask;
    }
}