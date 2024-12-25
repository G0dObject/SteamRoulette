using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SteamRoulette.Domain.Common;
using SteamRoulette.WebApi.DTO;
using SteamRoulette.WebApi.Services;
using System.Security.Claims;

namespace SteamRoulette.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class InventoryController(InventoryService inventoryService, Mapper mapper) : ControllerBase
    {

        [HttpGet(template: nameof(GetUserInventory))]
        public async Task<ICollection<SteamItemDTO>> GetUserInventory()
        {
            var nameClaim = User.Identities
                .Select(f => f.FindFirst(ClaimTypes.NameIdentifier))
                .FirstOrDefault(claim => claim != null);

            var internalItems = await inventoryService.GetUserItems(nameClaim.Value);
            return mapper.InventoryMapper.MapList(internalItems);
        }

        [HttpGet(template: nameof(GetUserInventoryById))]
        public async Task<ICollection<SteamItemDTO>> GetUserInventoryById([FromBody] string steamUserId)
        {
            var internalItems = await inventoryService.GetUserItems(steamUserId);
            return mapper.InventoryMapper.MapList(internalItems);
        }
    }
}
