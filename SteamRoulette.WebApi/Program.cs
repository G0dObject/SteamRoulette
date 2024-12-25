using Microsoft.AspNetCore.Identity;
using SteamRoulette.Domain;
using SteamRoulette.Infrastructure.Intefaces.Services;
using SteamRoulette.Persistanse;
using SteamRoulette.ServiceDefaults;
using SteamRoulette.WebApi.DTO;
using SteamRoulette.WebApi.Services;

namespace SteamRoulette.WebApi;
internal class Program
{
    private static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Logging.ClearProviders();
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

        builder.Services.AddHttpClient<SteamService>();
        builder.Services.AddSingleton<Game>();
        builder.Services.AddScoped<InventoryService>();
        builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        builder.Services.AddCors(opt =>
        {
            opt.AddPolicy("default", opt =>
            {
                opt.AllowAnyOrigin();
                opt.AllowAnyMethod();
                opt.AllowAnyHeader();
                opt.AllowCredentials()
                   .WithOrigins("http://localhost:5000");
            });
        });
        builder.Services.AddScoped<SteamService>();
        builder.Services.AddScoped<UserService>();
        builder.Services.AddSingleton<Mapper>();
        WebApplication app = builder.Build();

        var game = app.Services.GetRequiredService<Game>();
        var cancellationToken = app.Services.GetRequiredService<IHostApplicationLifetime>().ApplicationStopping;
        Task.Run(() => game.StartGameAsync(cancellationToken), cancellationToken);

        app.MapHealthChecks("/health");
        app.MapControllers();
        app.UseHttpsRedirection();
        app.UseSwagger();
        app.UseSwaggerUI();

        app.MapHub<GameHub>("/gameHub");

        app.UseCors("default");

        app.UseAuthentication();
        app.UseAuthorization();

        app.Run();
    }
}