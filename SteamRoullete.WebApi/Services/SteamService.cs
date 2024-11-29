using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SteamRoullete.WebApi.Services
{
    public class SteamService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public SteamService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> GetProfileImageUrlAsync(string steamId64)
        {
            var requestUrl = $"https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key={_configuration["Steam:KEY"]}&steamids={steamId64}";
            var response = _httpClient.GetStringAsync(requestUrl).Result;

            var jsonResponse = JObject.Parse(response);
            var avatarFull = jsonResponse["response"]["players"][0]["avatarfull"].ToString();

            return avatarFull;
        }
    }
}