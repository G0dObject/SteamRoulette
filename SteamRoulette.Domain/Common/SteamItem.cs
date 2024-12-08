namespace SteamRoulette.Domain.Common
{
    public class SteamItem
    {
        public int SteamItemId { get; set; }
        public decimal SteamItemPrice { get; set; } = 0;
        public string SteamItemImg { get; set; }
        public string SteamItemTitle { get; set; }

        public SteamUser? SteamUser { get; set; }
    }
}