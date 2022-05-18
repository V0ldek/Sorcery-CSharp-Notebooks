using Microsoft.EntityFrameworkCore;

namespace Adder.DataLayer;

public sealed class NumberRepository : INumberRepository
{
    private readonly AdderDbContext _dbContext;

    public NumberRepository(AdderDbContext dbContext) => _dbContext = dbContext;

    public async Task<int?> GetNumberAsync(string name)
    {
        var number = await _dbContext.Numbers.SingleOrDefaultAsync(n => n.Name == name);
        return number?.Value;
    }

    public async Task SeedAsync()
    {
        await _dbContext.Database.EnsureDeletedAsync();
        await _dbContext.Database.EnsureCreatedAsync();

        _dbContext.Numbers.Add(new("one", 1));
        _dbContext.Numbers.Add(new("two", 2));
        _dbContext.Numbers.Add(new("three", 3));
        _dbContext.Numbers.Add(new("four", 4));
        _dbContext.Numbers.Add(new("five", 5));

        await _dbContext.SaveChangesAsync();
    }
}