using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ShoppingApp.API.Constants;
using ShoppingApp.Core.Options;
using System.Text;

namespace ShoppingApp.API.Extensions;

internal static class AuthenticationExtensions
{
    /// <summary>
    /// Add authentication logic of Shopping App
    /// </summary>
    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, ops =>
                {
                    var jwtOptions = configuration.GetSection(OptionsNames.JwtOptions).Get<JwtOptions>()
                                     ?? throw new Exception("JwtOptions is not configured");

                    ops.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret))
                    };
                });

        return services;
    }
}
