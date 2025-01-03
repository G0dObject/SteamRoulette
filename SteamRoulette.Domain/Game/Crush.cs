namespace SteamRoulette.Domain.Game
{
    internal class Crush
    {
        private (double, double) _miniSpread = (1.01d, 2.4d);
        private (double, double) _middleSpread = (2.4d, 5d);
        private (double, double) _largeSpread = (5d, 13d);
        private (double, double) _massiveSpread = (13d, 100d);

        public CrushType Type { get; init; }
        public decimal Multiplayer { get; private set; }
        public Crush(CrushType type)
        {
            Type = type;
            Multiplayer = GenerateMultiplayer();
        }

        private decimal GenerateMultiplayer()
        {
            var random = new Random();
            return Type switch
            {
                CrushType.Unit => 1,
                CrushType.Mini => Round(random.NextDouble(_miniSpread.Item1, _miniSpread.Item2)),
                CrushType.Middle => Round(random.NextDouble(_middleSpread.Item1, _middleSpread.Item2)),
                CrushType.Large => Round(random.NextDouble(_largeSpread.Item1, _largeSpread.Item2)),
                CrushType.Massive => Round(random.NextDouble(_massiveSpread.Item1, _massiveSpread.Item2)),
                _ => 1,
            };
        }
        private decimal Round(double value, int accuracy = 2) =>
             Convert.ToDecimal(Math.Round(value, accuracy));
    }
}
