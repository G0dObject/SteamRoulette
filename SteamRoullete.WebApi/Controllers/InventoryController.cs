using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SteamRoullete.WebApi.DTO;
using SteamRoullete.WebApi.Services;

namespace SteamRoullete.WebApi.Controllers
{
    public class InventoryController(ILogger logger, InventoryService inventoryService, Mapper mapper)
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
