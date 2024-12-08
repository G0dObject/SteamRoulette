using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SteamRoullete.WebApi
{
    public static class Extension
    {
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            _ = services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = "Steam";
            })
     .AddJwtBearer(options =>
     {
         options.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuer = false,
             ValidateAudience = false,
             ValidateLifetime = false,
             ValidateIssuerSigningKey = false,
             ValidIssuer = "your-issuer", // Replace with your issuer
             ValidAudience = "your-audience", // Replace with your audience
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-secret-key"))
         };
     })
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