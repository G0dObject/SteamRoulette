using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SteamRoulette.WebApi.DTO;
using SteamRoulette.WebApi.Services;

namespace SteamRoulette.WebApi.Controllers
{
    public class InventoryController(InventoryService inventoryService, Mapper mapper)
    {
        [Authorize]
        [HttpGet]
        public async Task<List<SteamItemDTO>> GetUserInventory([FromBody] string steamUserId)
        {
            var internalItems = await inventoryService.GetUserItems(steamUserId);
            return mapper.InventoryMapper.MapList(internalItems);
        }
    }
}
