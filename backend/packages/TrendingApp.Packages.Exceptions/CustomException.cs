namespace TrendingApp.Packages.Exceptions;

/// <summary>
/// The base exception created specially for Trending App. This exception aids web APIs specially. Includes properties
/// such as <i>Url</i> and <i>HttpStatusCode</i>.
/// </summary>
public class CustomException(int? statusCode = null,
                             string? url = null,
                             string? message = null,
                             object? data = null,
                             Exception? innerException = null)
    : Exception(message, innerException)
{
    public int? HttpStatusCode { get; private set; } = statusCode;
    public string? Url { get; private set; } = url;
    public new object? Data { get; private set; } = data;
}