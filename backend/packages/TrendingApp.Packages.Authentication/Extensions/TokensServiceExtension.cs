using Microsoft.Extensions.DependencyInjection;
using TrendingApp.Packages.Authentication.Services;

namespace TrendingApp.Packages.Authentication.Extensions;

public static class TokensServiceExtension
{
    /// <summary>
    /// Consume the token service using the <see cref="ITokensService">ITokensService</see> interface
    /// </summary>
    public static IServiceCollection AddTokensService(this IServiceCollection services) =>
        services.AddScoped<ITokensService, TokensService>();
}
