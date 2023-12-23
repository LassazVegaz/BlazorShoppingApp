using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingApp.API.ExceptionHandlers;

public class UnknownExceptionHandler(ILogger<UnknownExceptionHandler> logger, IWebHostEnvironment env) : IExceptionHandler
{
    private readonly ILogger<UnknownExceptionHandler> _logger = logger;
    private readonly IWebHostEnvironment _env = env;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "An unknown error occurred.");

        // message is not sent to the client in production
        var msg = _env.IsDevelopment() ? exception.Message : null;
        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "An unknown error occurred.",
            Detail = msg,
        };

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
