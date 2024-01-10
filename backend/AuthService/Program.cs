using TrendingApp.Packages.ApiExceptionsHandler;
using TrendingApp.Packages.Authentication.Extensions;
using TrendingApp.Packages.Cors;
using TrendingApp.Packages.MassTransitDependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// cors
builder.Services.AddTrendingAppCors();

// Exception handlers
builder.Services.AddTrendingAppExceptionHandlers();

// auth
builder.Services.AddTrendingAppAuthentication();
builder.Services.AddTokensService();
builder.Services.AddAuthorization();

// MassTransit
builder.Services.AddTrendingAppMassTransit();


var app = builder.Build();

app.UseExceptionHandler(ops => { });

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
