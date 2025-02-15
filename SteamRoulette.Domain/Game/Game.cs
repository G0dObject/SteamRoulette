namespace SteamRoulette.Domain.Game
{
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading.Tasks;

    public sealed class Game(IHubContext<GameHub> hubContext, ILogger<Game> logger)
    {
        private double _fortune = 1; //The overall luck of the game is from 0 to 1

        private double _miniCrush = 0.40; //from 0 to 1
        private double _middleCrush = 0.13; //from 0 to 1
        private double _largeCrush = 0.04; //from 0 to 1
        private double _massiveCrush = 0.005; //from 0 to 1

        private Random _random = new Random();

        private CrushType CalculationType()
        {
            var randomValue = _random.NextDouble();

            var adjustedValues = (
                Mini: _miniCrush * (1 + _fortune),
                Middle: _middleCrush * (1 + _fortune * 2),
                Large: _largeCrush * (1 + _fortune * 3),
                Massive: _massiveCrush * (1 + _fortune * 4)
            );

            return randomValue switch
            {
                _ when randomValue <= adjustedValues.Massive => CrushType.Massive,
                _ when randomValue <= adjustedValues.Large => CrushType.Large,
                _ when randomValue <= adjustedValues.Middle => CrushType.Middle,
                _ when randomValue <= adjustedValues.Mini => CrushType.Mini,
                _ => CrushType.Unit
            };
        }

        public async Task StartGameAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await PrepareGame();
                await Task.Delay(TimeSpan.FromSeconds(10));

                var type = CalculationType();
                var crush = new Crush(type);
                double delay = CalculateDelay(crush.Multiplier);

                await StartGame((double)crush.Multiplier);

                logger.LogInformation("{0} {1}", crush.Multiplier, delay);

                await Task.Delay((int)(delay * 1000));

                await Crush(crush.Multiplier);
            }
        }

        private double CalculateDelay(decimal multiplayer)
        {
            double x = (double)multiplayer; // Преобразуем значение в double
            double a = 2.285; // Коэффициент a
            double b = 1.1133; // Степень b

            // Формула: delay = a * (x^b)
            return a * Math.Pow(x, b);
            //TODO проверка на еденицу при расчте времени
        }

        private async Task PrepareGame()
        {
            await hubContext.Clients.All.SendAsync("PreparationGame");
        }

        private async Task StartGame(double result)
        {
            await hubContext.Clients.All.SendAsync("Start");
            var cancellationTokenSource = new CancellationTokenSource();
            await SendNumberGrowthAsync(result, cancellationTokenSource.Token);
        }

        public async Task SendNumberGrowthAsync(double targetNumber, CancellationToken cancellationToken)
        {
            double a = 2.285;
            double b = 1.1133;

            double totalDelay = a * Math.Pow((double)targetNumber, b);
            double currentNumber = 1;
            double stepTime = 0.1;
            int steps = (int)(totalDelay / stepTime);
            var increment = (targetNumber - 1) / steps;

            for (int i = 0; i < steps; i++)
            {
                if (cancellationToken.IsCancellationRequested)
                    break;

                currentNumber += increment;
                await hubContext.Clients.All.SendAsync("UpdateNumber", currentNumber);
                await Task.Delay((int)(stepTime * 1000), cancellationToken);
            }

            await hubContext.Clients.All.SendAsync("UpdateNumber", targetNumber);
        }


        private async Task Crush(decimal value)
        {
            await hubContext.Clients.All.SendAsync("Crush", value);
        }
    }
}