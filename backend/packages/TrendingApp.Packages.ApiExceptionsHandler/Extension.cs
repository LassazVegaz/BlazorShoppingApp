using Microsoft.Extensions.DependencyInjection;

namespace TrendingApp.Packages.ApiExceptionsHandler;

public static class Extension
{
    public static IServiceCollection AddTrendingAppExceptionHandlers(this IServiceCollection services)
        => services.AddExceptionHandler<AppExceptionsHandler>()
                   .AddExceptionHandler<UnknownExceptionHandler>();
}
