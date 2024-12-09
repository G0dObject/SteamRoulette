using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using SteamRoulette.Domain;
using SteamRoulette.Persistanse;

namespace SteamRoulette.WebApi.Services
{
    public class SteamService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly SteamDbContext _steamDbContext;
        private readonly UserManager<SteamUser> _userManager;

        public SteamService(HttpClient httpClient, IConfiguration configuration, SteamDbContext steamDbContext, UserManager<SteamUser> userManager)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _steamDbContext = steamDbContext;
            _userManager = userManager;
        }

        public async Task<string> GetProfileImageUrlAsync(string steamId64)
        {
            var requestUrl = $"https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key={_configuration["Steam:KEY"]}&steamids={steamId64}";
            var response = _httpClient.GetStringAsync(requestUrl).Result;

            var jsonResponse = JObject.Parse(response);
            var avatarFull = jsonResponse["response"]["players"][0]["avatarfull"].ToString();

            return avatarFull;
        }

        public async Task<SteamUser> GetSteamUserBySteamId(string steamId)
        {
            return await _userManager.FindBySteamIdAsync(steamId);
        }

    }
}