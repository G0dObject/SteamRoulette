namespace SteamRoulette.Domain.Common
{
    public class RoundBet
    {
        public int RoundBetId { get; set; }
        public SteamUser? User { get; set; }
        public SteamItem? Item { get; set; }
    }
}
