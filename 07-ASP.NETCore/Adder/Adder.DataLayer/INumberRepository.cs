namespace Adder.DataLayer;

public interface INumberRepository
{
    Task<int?> GetNumberAsync(string name);

    Task SeedAsync();
}
