using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ShoppingApp.API.Constants;
using ShoppingApp.API.ExceptionHandlers;
using ShoppingApp.API.Extensions;
using ShoppingApp.API.Mapper;
using ShoppingApp.Core.Data;
using ShoppingApp.Core.Options;
using ShoppingApp.Core.Services;
using ShoppingApp.Logic.Services;

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
    ops.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddAuthorization();

// db context
var mySqlVersion = new MySqlServerVersion(builder.Configuration.GetValue("MySQLVersion", "8.0.29"));
builder.Services.AddDbContext<ShoppingAppContext>(ops =>
    ops.UseMySql(builder.Configuration.GetConnectionString("ShoppingAppDb"), mySqlVersion));

// mapper
builder.Services.AddAutoMapper(typeof(MapperProfile));

// app services
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IAuthService, AuthService>();

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
