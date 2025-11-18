using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SteamRoulette.Domain;
using SteamRoulette.Infrastructure.Intefaces.Services;
using SteamRoulette.Persistence;
using SteamRoulette.ServiceDefaults;
using SteamRoulette.WebApi.DTO;
using SteamRoulette.WebApi.Services;

namespace SteamRoulette.WebApi;
internal class Program
{
    private static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Logging.AddConsole();
        builder.Services.AddDefaultServices();

        builder.Services.AddCustomAuthentication(builder.Configuration);

        builder.Services.AddHealthChecks();
        builder.AddServiceDefaults();
        builder.Services.AddDbContext<SteamDbContext>(opt => opt.EnableSensitiveDataLogging());
        builder.Services.AddIdentity<SteamUser, IdentityRole<int>>(options =>
        {
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_-.[ ]абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
            options.User.RequireUniqueEmail = false;

            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 0;
        })
        .AddEntityFrameworkStores<SteamDbContext>()
        .AddDefaultTokenProviders();
        builder.Services.AddSignalR();

        builder.Services.AddHttpClient<SteamService>(client =>
        {
            client.Timeout = TimeSpan.FromSeconds(30);
            client.DefaultRequestHeaders.Add("User-Agent", "SteamRoulette/1.0");
        });
        builder.Services.AddSingleton<Game>();
        builder.Services.AddScoped<InventoryService>();
        builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() 
            ?? new[] { "http://localhost:5173", "http://localhost:5000", "https://localhost:5173" };
        
        builder.Services.AddCors(opt =>
        {
            opt.AddPolicy("default", policy =>
            {
                policy.WithOrigins(allowedOrigins)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });
        builder.Services.AddScoped<SteamService>();
        builder.Services.AddScoped<UserService>();
        builder.Services.AddSingleton<Mapper>();

        WebApplication app = builder.Build();

        // Apply migrations
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<SteamDbContext>();
            dbContext.Database.Migrate();
        }

        app.UseHttpsRedirection();
        app.UseCors("default");
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapHealthChecks("/health");
        app.MapControllers();
        app.MapHub<GameHub>("/gamehub");
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.Run();
    }
}