namespace SteamRoullete.WebApi.DTO
{
    public class SteamItemDTO
    {
        public int SteamItemId { get; set; }
        public decimal SteamItemPrice { get; set; } = 0;
        public string SteamItemImg { get; set; }
        public string SteamItemTitle { get; set; }

        public string? SteamUserId { get; set; }
    }
}
