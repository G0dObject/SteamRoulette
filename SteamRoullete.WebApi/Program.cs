using Microsoft.AspNetCore.Identity;
using SteamRoulette.Persistanse;
using SteamRoullete.WebApi;
using SteamRoullete.WebApi.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();
builder.Services.AddDbContext<MyDbContext>();
builder.Services.AddDefaultServices();
builder.Services.AddJwtBearerAuthentication();
builder.Services.AddScoped<JwtTokenGenerator>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>(opt =>
{
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredLength = 4;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireDigit = false;
    opt.Password.RequireNonAlphanumeric = false;
}).AddEntityFrameworkStores<MyDbContext>().AddDefaultTokenProviders();

WebApplication app = builder.Build();

app.MapControllers();
app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.Run();