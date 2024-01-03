namespace ShoppingApp.Core.Exceptions;

/// <summary>
/// In a Web API, this exception can be considered as a 400 Bad Request.
/// </summary>
public class BadArgumentsException(string? message = null, object? data = null)
    : CustomException(400, "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1", message, data)
{
}
