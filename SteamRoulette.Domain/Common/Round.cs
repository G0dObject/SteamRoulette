namespace SteamRoulette.Domain.Common
{
    public class Round
    {
        public int RoundId { get; set; }
        public Guid RoundHash { get; set; } = Guid.NewGuid();
        public List<RoundBet>? Bets { get; set; }
    }
}
