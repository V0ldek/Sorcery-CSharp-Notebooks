using CSharpFunctionalExtensions;

namespace ClassLib;

public class Service
{
    private readonly IRepository _repository;

    public Service(IRepository repository) => _repository = repository;

    public async Task<Result<Item?, Exception>> Query(string id)
    {
        ArgumentNullException.ThrowIfNull(id);

        try
        {
            var result = await _repository.GetByIdAsync(id);
            return Result.Success<Item?, Exception>(result);
        }
        catch (Exception exception)
        {
            return Result.Failure<Item?, Exception>(exception);
        }
    }
}
