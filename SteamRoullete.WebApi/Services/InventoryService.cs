using Microsoft.EntityFrameworkCore;
using SteamRoulette.Domain.Common;
using SteamRoulette.Persistanse;

namespace SteamRoullete.WebApi.Services
{
    public class InventoryService
    {
        private readonly SteamService _steamService;
        private readonly SteamDbContext _steamDbContext;
        public InventoryService(SteamService steamService, SteamDbContext steamDbContext)
        {
            _steamService = steamService;
            _steamDbContext = steamDbContext;
        }

        public async Task<List<SteamItem>> GetUserItems(string steamId)
        {
            var user = await _steamService.GetSteamUserBySteamId(steamId);
            var items = await _steamDbContext.SteamItems.Where(f => f.SteamUser.SteamUserId == steamId).ToListAsync();

            return items;
        }
    }
}
