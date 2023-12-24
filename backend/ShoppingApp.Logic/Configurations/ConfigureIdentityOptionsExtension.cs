using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ShoppingApp.Logic.Configurations;

public static class ConfigureIdentityOptionsExtension
{
    public static IServiceCollection ConfigureIdentityOptions(this IServiceCollection services)
    {
        services.Configure<IdentityOptions>(ops =>
        {
            ops.User.RequireUniqueEmail = true;
        });

        return services;
    }
}
