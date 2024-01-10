using AuthService;
using AuthService.Core;
using Microsoft.EntityFrameworkCore;
using TrendingApp.Packages.ApiExceptionsHandler;
using TrendingApp.Packages.Authentication.Extensions;
using TrendingApp.Packages.Cors;
using TrendingApp.Packages.MassTransitDependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// db context
builder.Services.AddDbContext<AuthServiceContext>(ops =>
{
    var consStr = builder.Configuration.GetConnectionString("AuthServiceDb");
    var mySqlVersion = builder.Configuration.GetValue("MySQLVersion", "8.0.29");
    ops.UseMySql(consStr, new MySqlServerVersion(mySqlVersion));
});

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

// auto mapper
builder.Services.AddAutoMapper(typeof(Mapper));

// app services
builder.Services.AddScoped<IAuthManager, AuthManager>();


var app = builder.Build();

app.UseExceptionHandler(ops => { });

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
