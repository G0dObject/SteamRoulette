
using SteamRoulette.Domain.Common;

namespace SteamRoulette.Infrastructure.Intefaces.Services
{
    public interface IInventoryService
    {
        Task<List<SteamItem>> GetUserItems(string steamId);

        Task<SteamItem?> GetItemById(int itemId);
    }
}