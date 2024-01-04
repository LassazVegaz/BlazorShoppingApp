using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TrendingApp.Packages.Authentication.Extensions;

public static class AuthenticationExtension
{
    private static readonly TokenValidationParameters tokenValidationParameters = new()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,

        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.SigningKey)),
    };


    /// <summary>
    /// Read <see href="./README.md" >README.md</see> in the project repo for more information
    /// </summary>
    public static IServiceCollection AddTrendingAppAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, ops =>
                {
                    ops.TokenValidationParameters = tokenValidationParameters;

                    ops.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = async context =>
                        {
                            if (context.SecurityToken is not JsonWebToken headerToken)
                                throw new Exception("Invalid token implementation");

                            context.HttpContext.Request.Cookies.TryGetValue(Constants.CookieName, out var cookieToken);
                            if (cookieToken is null) return;
                            else if (headerToken.EncodedToken != cookieToken) context.Fail("CSRF detected");

                            await Task.CompletedTask;
                        }
                    };
                });

        return services;
    }
}
