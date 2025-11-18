using Microsoft.AspNetCore.SignalR;

namespace SteamRoulette.WebApi
{
    public class Game
    {
        private readonly IHubContext<GameHub> _hubContext;
        private readonly Random _random;
        private readonly double _baseCrashChance;
        private bool _isRunning;
        private double _currentMultiplier;

        public Game(IHubContext<GameHub> hubContext)
        {
            _hubContext = hubContext;
            _random = new Random();
            _baseCrashChance = 0.01; // Базовая вероятность краха (1%)
            _isRunning = false;
            _currentMultiplier = 1.0;
        }

        public async Task StartGameAsync(CancellationToken cancellationToken)
        {
            _isRunning = true;
            _currentMultiplier = 1.0;
            const double increment = 0.01; // Шаг увеличения множителя
            const int updateIntervalMs = 50; // Обновление каждые 50мс для плавности

            while (_isRunning && !cancellationToken.IsCancellationRequested)
            {
                // Вычисляем вероятность краха на основе текущего множителя
                // Чем выше множитель, тем выше вероятность краха
                double crashProbability = _baseCrashChance * _currentMultiplier;

                // Проверяем, произошел ли крах
                if (_random.NextDouble() < crashProbability)
                {
                    await _hubContext.Clients.All.SendAsync("Crush", _currentMultiplier, cancellationToken);
                    _isRunning = false;
                    break;
                }

                // Увеличиваем множитель
                _currentMultiplier += increment;

                // Отправка текущего множителя клиентам
                await _hubContext.Clients.All.SendAsync("ReceiveNumber", Math.Round(_currentMultiplier, 2), cancellationToken);

                // Задержка для имитации времени раунда
                await Task.Delay(updateIntervalMs, cancellationToken);
            }
        }

        public void StopGame()
        {
            _isRunning = false;
        }

        public double GetCurrentMultiplier()
        {
            return _currentMultiplier;
        }
    }
}
