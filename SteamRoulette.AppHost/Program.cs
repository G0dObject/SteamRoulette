using Microsoft.Extensions.Hosting;


var frontendport = 5000;

var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var webapi = builder.AddProject<Projects.SteamRoulette_WebApi>("WebApi");

var frontend = builder.AddNpmApp("frontend", "../SteamRoulette.FrontEnd", "dev")
    .WithReference(webapi)
    .WithReference(cache)
    .WithHttpEndpoint(env: "VITE_PORT", port: frontendport)
    .WithEnvironment("VITE_CURRENT_PORT", frontendport.ToString())
    .WithExternalHttpEndpoints()
.PublishAsDockerFile();

var launchProfile = builder.Configuration["DOTNET_LAUNCH_PROFILE"] ??
    builder.Configuration["AppHost:DefaultLaunc hProfileName"];

if (builder.Environment.IsDevelopment() && launchProfile == "https")
{
    frontend.WithEnvironment("NODE_TLS_REJECT_UNAUTHORIZED", "0");
}

builder.Build().Run();