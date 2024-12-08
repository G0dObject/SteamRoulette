using Microsoft.AspNetCore.SignalR;

namespace SteamRoullete.WebApi
{
    public class Game
    {
        private readonly IHubContext<GameHub> _hubContext;
        private readonly Random _random;
        private readonly double _crushThreshold;
        private bool _isRunning;

        public Game(IHubContext<GameHub> hubContext)
        {
            _hubContext = hubContext;
            _random = new Random();
            _crushThreshold = 0.9; // Пример порога для краха
            _isRunning = false;
        }

        public async Task StartGameAsync(CancellationToken cancellationToken)
        {
            _isRunning = true;
            double currentNumber = 0;

            while (_isRunning && !cancellationToken.IsCancellationRequested)
            {
                // Генерация случайного числа
                currentNumber += _random.NextDouble();

                // Отправка текущего числа клиентам
                await _hubContext.Clients.All.SendAsync("ReceiveNumber", currentNumber, cancellationToken);

                // Проверка на крах
                if (currentNumber > _crushThreshold)
                {
                    await _hubContext.Clients.All.SendAsync("Crush", cancellationToken);
                    _isRunning = false;
                }

                // Задержка для имитации времени раунда
                await Task.Delay(1000, cancellationToken);
            }
        }

        public void StopGame()
        {
            _isRunning = false;
        }
    }
}
