namespace SteamRoulette.Domain.Game
{
    using Microsoft.AspNetCore.SignalR;
    using System.Threading.Tasks;

    public sealed class Game
    {
        private readonly IHubContext<GameHub> _hubContext;
        private double _fortune = 1; //The overall luck of the game is from 0 to 1
        private double _miniCrush = 0.40; //from 0 to 1
        private double _middleCrush = 0.13; //from 0 to 1
        private double _largeCrush = 0.04; //from 0 to 1
        private double _massiveCrush = 0.005; //from 0 to 1

        private Random _random;
        public Game(IHubContext<GameHub> hubContext)
        {
            _hubContext = hubContext;
            _random = new Random();
        }



        private CrushType CalculationType()
        {
            var randomValue = _random.NextDouble();
            double adjustedMini = _miniCrush * (1 + _fortune);
            double adjustedMiddle = _middleCrush * (1 + _fortune * 2);
            double adjustedLarge = _largeCrush * (1 + _fortune * 3);
            double adjustedMassive = _massiveCrush * (1 + _fortune * 4);

            if (randomValue <= adjustedMassive)
                return CrushType.Massive;
            else if (randomValue <= adjustedLarge)
                return CrushType.Large;
            else if (randomValue <= adjustedMiddle)
                return CrushType.Middle;
            else if (randomValue <= adjustedMini)
                return CrushType.Mini;
            else
                return CrushType.Unit;
        }

        public async Task StartGameAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await PrepareGame();
                await Task.Delay(10000);
                await StartGame();
                var type = CalculationType();
                var crush = new Crush(type);
                await _hubContext.Clients.All.SendAsync("Crush", crush.Multiplayer, cancellationToken);
            }
        }

        private async Task PrepareGame()
        {
            await _hubContext.Clients.All.SendAsync("PreparationGame");
        }

        private async Task StartGame()
        {
            await _hubContext.Clients.All.SendAsync("Start");
        }

        private async Task Crush(decimal value)
        {
            await _hubContext.Clients.All.SendAsync("Crush", value);
        }
    }
}