using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace UsersService.API.ExceptionHandlers;

public class UnknownExceptionHandler(ILogger<UnknownExceptionHandler> logger, IWebHostEnvironment env) : IExceptionHandler
{
    private readonly ILogger<UnknownExceptionHandler> _logger = logger;
    private readonly IWebHostEnvironment _env = env;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "An unknown error occurred.");

        // Exception message is not sent to the client in production
        var msg = _env.IsDevelopment()
            ? exception.Message
            : "An internal server error occured. Please contact support.";

        // stack trace is sent as an extension in development
        var extensions = _env.IsDevelopment()
            ? new Dictionary<string, object?>()
            {
                [nameof(exception.StackTrace)] = exception.StackTrace,
            } : [];

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "An unknown error occurred.",
            Detail = msg,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            Extensions = extensions,
        };

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
