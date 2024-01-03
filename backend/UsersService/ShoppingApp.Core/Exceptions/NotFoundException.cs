namespace UsersService.Core.Exceptions;

// In a Web API, this exception can be considered as a 404 Not Found.
public class NotFoundException(string? message = null, object? data = null)
    : CustomException(404, "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4", message, data)
{
}
