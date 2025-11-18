using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using SteamRoulette.Domain;
using SteamRoulette.Persistence;

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
            try
            {
                var steamKey = _configuration["Steam:Key"] ?? _configuration["Steam:KEY"];
                if (string.IsNullOrEmpty(steamKey))
                {
                    throw new InvalidOperationException("Steam API Key is not configured");
                }

                var requestUrl = $"https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key={steamKey}&steamids={steamId64}";
                
                var response = await _httpClient.GetStringAsync(requestUrl);

                if (string.IsNullOrEmpty(response))
                {
                    return string.Empty;
                }

                var jsonResponse = JObject.Parse(response);
                var players = jsonResponse["response"]?["players"] as JArray;
                
                if (players == null || players.Count == 0)
                {
                    return string.Empty;
                }

                var avatarFull = players[0]?["avatarfull"]?.ToString() ?? string.Empty;
                return avatarFull;
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException($"Failed to retrieve Steam profile: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving Steam profile image: {ex.Message}", ex);
            }
        }

        public async Task<SteamUser?> GetSteamUserBySteamId(string steamId)
        {
            return await _userManager.FindBySteamIdAsync(steamId);
        }

    }
}