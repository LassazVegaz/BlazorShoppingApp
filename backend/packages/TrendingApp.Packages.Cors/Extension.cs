using Microsoft.Extensions.DependencyInjection;

namespace TrendingApp.Packages.Cors;

public static class Extension
{
    public static IServiceCollection AddTrendingAppCors(this IServiceCollection services)
        => services.AddCors(ops => ops.AddDefaultPolicy(policy => policy.WithOrigins(Constants.FrontEndUrl)
                                                                        .AllowAnyHeader()
                                                                        .AllowAnyMethod()
                                                                        .AllowCredentials()));
}
