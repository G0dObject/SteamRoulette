namespace SteamRoulette.Domain.Common
{
    public class SteamItem
    {
        public int SteamItemId { get; set; }
        public decimal SteamItemPrice { get; set; } = 0;
        public string SteamItemImg { get; set; } = string.Empty;
        public string SteamItemTitle { get; set; } = string.Empty;

        public SteamUser? SteamUser { get; set; }
    }
}