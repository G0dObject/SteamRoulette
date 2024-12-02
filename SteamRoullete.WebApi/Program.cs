using Microsoft.AspNetCore.Identity;
using SteamRoulette.Domain;
using SteamRoulette.Persistanse;
using SteamRoullete.WebApi;
using SteamRoullete.WebApi.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Logging.AddConsole();
        builder.Services.AddDefaultServices();

        builder.Services.AddCustomAuthentication(builder.Configuration);
        builder.Services.AddHealthChecks();
        builder.Services.AddDbContext<MyDbContext>(opt => opt.EnableSensitiveDataLogging());
        builder.Services.AddIdentity<SteamUser, IdentityRole<int>>(options =>
        {
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_-.[ ]абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
            options.User.RequireUniqueEmail = false; // Ñäåëàòü email íåîáÿçàòåëüíûì

            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 0;
        })
        .AddEntityFrameworkStores<MyDbContext>()
        .AddDefaultTokenProviders();



        builder.Services.AddCors(opt =>
        {
            opt.AddPolicy("default", opt =>
            {
                opt.AllowAnyOrigin();
                opt.AllowAnyMethod();
                opt.AllowAnyHeader();
            });
        });
        builder.Services.AddHttpClient<SteamService>();
        builder.Services.AddScoped<SteamService>();
        builder.Services.AddScoped<UserService>();

        WebApplication app = builder.Build();

        app.MapHealthChecks("/health");
        app.MapControllers();
        app.UseHttpsRedirection();
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseCors("default");

        app.UseAuthentication();
        app.UseAuthorization();

        app.Run();
    }
}