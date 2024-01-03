using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UsersService.API.Constants;
using UsersService.API.Extensions;
using UsersService.Core.Options;

namespace UsersService.API.Extensions;

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

                    ops.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = async context =>
                        {
                            if (context.SecurityToken is not JsonWebToken headerToken)
                                throw new Exception("Invalid token implementation");

                            context.HttpContext.Request.Cookies.TryGetValue(CookieNames.JwtToken, out var cookieToken);
                            if (cookieToken is null) return;
                            else if (headerToken.EncodedToken != cookieToken) context.Fail("CSRF detected");

                            // Note: this API allows authorization via cookie or header
                            // It is recomemnded to use cookie when possible
                            // If cookie is used, JWT should be in Authorization header as well to prevent CSRF
                            // Either way, the token is in the header
                            // Therefore let the libray validate the token in the header
                            // That is why OnTokenValidated event is used here
                            // If token is in cookie too, the client is using cookie based authentication
                            // In that case, it should match with the token in the header
                            // If not, an attacker has attached the header
                            // The cookie cannot be attached by an attacker because it is HttpOnly

                            await Task.CompletedTask;
                        }
                    };
                });

        return services;
    }
}
