using Microsoft.EntityFrameworkCore;
using SteamRoulette.Domain.Common;
using SteamRoulette.Infrastructure.Intefaces.Services;
using SteamRoulette.Persistence;

namespace SteamRoulette.WebApi.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly SteamService _steamService;
        private readonly SteamDbContext _steamDbContext;
        public InventoryService(SteamService steamService, SteamDbContext steamDbContext)
        {
            _steamService = steamService;
            _steamDbContext = steamDbContext;
        }

        public async Task<SteamItem?> GetItemById(int itemId)
        {
            return await _steamDbContext.SteamItems.FindAsync(itemId);
        }

        public async Task<List<SteamItem>> GetUserItems(string steamId)
        {
            var user = await _steamService.GetSteamUserBySteamId(steamId);
            if (user == null)
            {
                return new List<SteamItem>();
            }
            
            var items = await _steamDbContext.SteamItems
                .Where(f => f.SteamUser != null && f.SteamUser.SteamUserId == steamId)
                .ToListAsync();

            return items;
        }
    }
}
