using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TrendingApp.Packages.Authentication.Extensions;
using UsersService.API.Constants;
using UsersService.API.ExceptionHandlers;
using UsersService.API.Mapper;
using UsersService.Core.Data;
using UsersService.Core.Options;
using UsersService.Core.Services;
using Services = UsersService.Logic.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// options
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(OptionsNames.JwtOptions));
builder.Services.Configure<UserOptions>(builder.Configuration.GetSection(OptionsNames.UserOptions));

// cors
builder.Services.AddCors(ops =>
{
    var frontEndUrl = builder.Configuration.GetValue<string>(ConfigurationKeys.FrontEndUrl)
                      ?? throw new Exception("FrontEndUrl is not configured");
    ops.AddDefaultPolicy(policy => policy.WithOrigins(frontEndUrl)
                                         .AllowAnyHeader()
                                         .AllowAnyMethod()
                                         .AllowCredentials());
});

// Exception handlers
builder.Services.AddExceptionHandler<AppExceptionsHandler>();
builder.Services.AddExceptionHandler<UnknownExceptionHandler>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(ops =>
{
    ops.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme.",
    });
});

// authentication
builder.Services.AddTrendingAppAuthentication();
builder.Services.AddTokensService();

builder.Services.AddAuthorization();

// db context
var mySqlVersion = new MySqlServerVersion(builder.Configuration.GetValue("MySQLVersion", "8.0.29"));
builder.Services.AddDbContext<ShoppingAppContext>(ops =>
    ops.UseMySql(builder.Configuration.GetConnectionString("ShoppingAppDb"), mySqlVersion));

// mapper
builder.Services.AddAutoMapper(typeof(MapperProfile));

// app services
builder.Services.AddScoped<IUsersService, Services.UsersService>();
builder.Services.AddScoped<IAuthService, Services.AuthService>();

var app = builder.Build();

app.UseExceptionHandler(ops => { });

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
