namespace ShoppingApp.Core.Exceptions;

/// <summary>
/// statusCode can be considered as a little support given by this layer for web API layers, but this is not bound to
/// web API layers only.
/// </summary>
public class CustomException(int? statusCode = null,
                             string? url = null,
                             string? message = null,
                             object? data = null) : Exception(message)
{
    public int? StatusCode { get; private set; } = statusCode;
    public string? Url { get; private set; } = url;
    public new object? Data { get; private set; } = data;
}