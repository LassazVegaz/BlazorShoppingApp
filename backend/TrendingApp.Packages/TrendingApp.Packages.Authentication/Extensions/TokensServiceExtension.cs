using Microsoft.Extensions.DependencyInjection;
using TrendingApp.Packages.Authentication.Services;

namespace TrendingApp.Packages.Authentication.Extensions;

public static class TokensServiceExtension
{
    public static IServiceCollection AddTokensService(this IServiceCollection services) =>
        services.AddScoped<ITokensService, TokensService>();
}
