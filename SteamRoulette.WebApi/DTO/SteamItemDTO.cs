namespace SteamRoulette.WebApi.DTO
{
    public class SteamItemDTO
    {
        public int SteamItemId { get; set; }
        public decimal SteamItemPrice { get; set; } = 0;
        public string SteamItemImg { get; set; } = string.Empty;
        public string SteamItemTitle { get; set; } = string.Empty;

        public string? SteamUserId { get; set; }
    }
}
