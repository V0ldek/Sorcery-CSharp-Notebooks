using System.Net;

namespace DungeonWalker.DataLayer;


public readonly struct ApiError
{
    private readonly HttpStatusCode _code = HttpStatusCode.BadRequest;

    public HttpStatusCode Code
    {
        get => _code == 0 ? HttpStatusCode.BadRequest : _code;
        init => _code = value;
    }

    public string Message { get; }

    public ApiError(string message) => Message = message;
}