using System.Net;

namespace RealState.Application.Features;

public class BaseCommandResponse<T> where T : class
{
    public HttpStatusCode StatusCode { get; set; }
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string[]? Errors { get; set; }
}
