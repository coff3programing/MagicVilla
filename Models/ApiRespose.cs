using System.Net;

namespace MagicVilla_API.Models;

public class ApiRespose
{
    public HttpStatusCode statusCode { get; set; }
    public bool IsSuccessful { get; set; } = true;
    public List<string>? ErrorMessages { get; set; }
    public object? Result { get; set; }
}
