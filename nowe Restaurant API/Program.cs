using nowe_Restaurant_API;
using nowe_Restaurant_API.Entities;
using nowe_Restaurant_API.services;
using NLog.Web;
using nowe_Restaurant_API.Middleware;
using nowe_Restaurant_API.Authorization;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using nowe_Restaurant_API.Models;
using nowe_Restaurant_API.Models.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var authenication = new AuthenticationSettings();
builder.Configuration.GetSection("Autentication").Bind(authenication);
builder.Services.AddSingleton(authenication);
// Add services to the container.
builder.Services.AddAuthentication(option =>
    {
        option.DefaultAuthenticateScheme = "Bearer";
        option.DefaultScheme = "Bearer";
        option.DefaultChallengeScheme = "Bearer";

    }).AddJwtBearer(cfg =>
    {
        cfg.RequireHttpsMetadata = false;
        cfg.SaveToken = true;
        cfg.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
        {
            ValidIssuer = authenication.JwtIssuer,
            ValidAudience = authenication.JwtIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenication.JwtKey)),

        };
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("HasNationality", builder => builder.RequireClaim("Nationality", "German", "Polish", "Poland"));
    options.AddPolicy("Has20", builder => builder.AddRequirements(new MinimumAgeRequiment(20)));
    });

builder.Services.AddScoped<IAuthorizationHandler, MinimumAgeRequimentHandler>();
builder.Services.AddScoped<IAuthorizationHandler, ResourceOperationRequimentHandler>();
builder.Services.AddScoped<IAuthorizationHandler, MinimumRestaurantsRequirementHandler>();
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddDbContext<RestaurantDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("RestaurantDbConnection")));

builder.Services.AddScoped<RestaurantSeeder>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IRestaurantServices, RestaurantServices>();
builder.Services.AddScoped<IDishServices, DishServices>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IValidator<UserDto>, UserDtoValidator>();
builder.Services.AddScoped<IValidator<SearchQuery>, SearchQueryValidator>();
builder.Services.AddScoped<ErrnorHandingMiddleware>();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<RequestTimeMiddleware>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontEndClient", builder =>
    {
        builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();// origin - powinna byæ konktretna domena klienta
    });
});

//nlog
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();

var app = builder.Build();



// Configure the HTTP request pipeline.
app.UseResponseCaching();
app.UseStaticFiles();
app.UseCors("FrontEndClient");
app.UseMiddleware<ErrnorHandingMiddleware>();
app.UseMiddleware<RequestTimeMiddleware>();
app.UseAuthentication();
app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "new Restaurant API");
});

app.UseAuthorization();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetService<RestaurantSeeder>();
seeder.seed();

app.MapControllers();

app.Run();
