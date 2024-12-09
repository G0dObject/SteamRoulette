using Microsoft.AspNetCore.Identity;
using SteamRoulette.Domain;
using SteamRoulette.Persistanse;
using SteamRoulette.ServiceDefaults;
using SteamRoulette.WebApi;
using SteamRoulette.WebApi.DTO;
using SteamRoulette.WebApi.Services;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();
Microsoft.Extensions.Hosting.DefaultExtensions.AddDefaultServices(builder.Services);

builder.Services.AddCustomAuthentication(builder.Configuration);

builder.Services.AddHealthChecks();
builder.AddServiceDefaults();
builder.Services.AddDbContext<SteamDbContext>(opt => opt.EnableSensitiveDataLogging());
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
.AddEntityFrameworkStores<SteamDbContext>()
.AddDefaultTokenProviders();
builder.Services.AddSignalR();

builder.Services.AddHttpClient<SteamService>();
builder.Services.AddSingleton<Game>();
builder.Services.AddScoped<InventoryService>();
builder.Services.AddScoped<JwtTokenGenerator>();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("default", opt =>
    {
        opt.AllowAnyOrigin();
        opt.AllowAnyMethod();
        opt.AllowAnyHeader();
    });
});
builder.Services.AddScoped<SteamService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddSingleton<Mapper>();

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
