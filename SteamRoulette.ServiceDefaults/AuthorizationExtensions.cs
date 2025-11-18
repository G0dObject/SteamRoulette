
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SteamRoulette.ServiceDefaults
{
    static public class AuthorizationExtensions
    {
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            _ = services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
     .AddJwtBearer(options =>
     {
         options.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuer = false,
             ValidateAudience = false,
             ValidateLifetime = false,
             ValidateIssuerSigningKey = false,
             ValidIssuer = configuration["Jwt:Issuer"],
             ValidAudience = configuration["Jwt:Issuer"],
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
         };
     })
     .AddSteam(options =>
     {
         options.SaveTokens = true;
         options.ApplicationKey = configuration["Steam:Key"] ?? configuration["Steam:KEY"];
         
         // Обработка событий для отладки
         options.Events.OnRemoteFailure = context =>
         {
             var error = context.Failure?.Message ?? "Unknown error";
             context.Response.Redirect($"/api/Auth/Login?error={Uri.EscapeDataString(error)}");
             context.HandleResponse();
             return Task.CompletedTask;
         };
     });
            return services;
        }

    }
}
