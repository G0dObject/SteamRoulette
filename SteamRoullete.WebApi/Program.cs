using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SteamRoulette.Domain;
using SteamRoulette.Persistanse;
using SteamRoullete.WebApi;
using SteamRoullete.WebApi.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();
builder.Services.AddDefaultServices();

builder.Services.AddCustomAuthentication(builder.Configuration);
builder.Services.AddHealthChecks();
builder.Services.AddDbContext<MyDbContext>(opt => opt.EnableSensitiveDataLogging());
builder.Services.AddIdentity<SteamUser, IdentityRole<int>>(options =>
{
    // Настройки для имени пользователя
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";
    options.User.RequireUniqueEmail = false; // Сделать email необязательным

    // Отключаем требования к паролю
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 0;
})
.AddEntityFrameworkStores<MyDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddHttpClient<SteamService>();
builder.Services.AddScoped<SteamService>();
builder.Services.AddScoped<UserService>();

WebApplication app = builder.Build();

app.MapHealthChecks("/health");
app.MapControllers();
app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.Run();