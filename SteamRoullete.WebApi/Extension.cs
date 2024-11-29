using Microsoft.AspNetCore.Authentication.Cookies;

namespace SteamRoullete.WebApi
{
    public static class Extension
    {
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            _ = services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "Steam";
            })
     .AddCookie("Cookies")
     .AddSteam(options =>
     {
         options.SaveTokens = true;

         options.ApplicationKey = configuration["Steam:Key"];
     });
            return services;
        }

        public static IServiceCollection AddDefaultServices(this IServiceCollection services)
        {
            _ = services.AddEndpointsApiExplorer();
            _ = services.AddSwaggerGen();
            _ = services.AddControllers();

            return services;
        }
    }
}