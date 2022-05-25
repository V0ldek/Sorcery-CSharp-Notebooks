namespace ClassLib;

public interface IRepository
{
    public Task<Item?> GetByIdAsync(string id);
}