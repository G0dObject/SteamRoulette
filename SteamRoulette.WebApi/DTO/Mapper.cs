using SteamRoulette.Domain.Common;
using SteamRoulette.WebApi.DTO;

namespace SteamRoulette.WebApi.DTO
{
    public class Mapper
    {
        public InventoryMapper InventoryMapper { get; set; } = new InventoryMapper();
    }
}

public class InventoryMapper
{
    public SteamItemDTO Map(SteamItem steamItem)
    {
        return new SteamItemDTO
        {
            SteamItemId = steamItem.SteamItemId,
            SteamItemPrice = steamItem.SteamItemPrice,
            SteamItemImg = steamItem.SteamItemImg,
            SteamItemTitle = steamItem.SteamItemTitle,
            SteamUserId = steamItem.SteamUser?.Id.ToString()
        };
    }

    public List<SteamItemDTO> MapList(List<SteamItem> list)
    {
        var items = new List<SteamItemDTO>();
        list.ForEach(i => items.Add(this.Map(i)));
        return items;
    }


}
