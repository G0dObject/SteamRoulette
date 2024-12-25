using Microsoft.AspNetCore.SignalR;

namespace SteamRoulette.WebApi
{
    public class Game(IHubContext<GameHub> hubContext, ILogger<Game> logger) : IDisposable
    {
        private readonly Random _random = new Random();
        private readonly double _maxThreshold = 3; // Максимальное значение, при котором игра может завершиться
        private bool _isRunning = false;
        private bool _disposed = false;
        private int _gameCounter = 0; // Счетчик игр
        private readonly double _trainProbability = 0.01; // Вероятность "паровоза" (примерно раз в 50-100 игр)
        private readonly double _zeroCrashProbability = 0.1; // Вероятность краха на нуле (примерно раз в 10 игр)
        private readonly int _minRoundTime = 200; // Минимальное время раунда в миллисекундах
        private readonly int _maxRoundTime = 1000; // Максимальное время раунда в миллисекундах
        private double _globalWinProbability = 0.01; // Общая вероятность победы (по умолчанию 95%)

        public async Task StartGameAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                // Добавляем 10-секундный таймаут перед началом игры
                await hubContext.Clients.All.SendAsync("PreparationGame", cancellationToken);

                await Task.Delay(10000, cancellationToken);
                await hubContext.Clients.All.SendAsync("Start", cancellationToken);

                _isRunning = true;
                double currentNumber = 1.0; // Начальное значение - 1.0
                _gameCounter++; // Увеличиваем счетчик игр

                logger.LogInformation("Game started.");

                // Проверка на крах на нуле
                if (_random.NextDouble() < _zeroCrashProbability)
                {
                    await hubContext.Clients.All.SendAsync("Crush", 1.00, cancellationToken);
                    logger.LogWarning("Game ended due to a crash at 0.00.");
                    _isRunning = false;
                }

                while (_isRunning && !cancellationToken.IsCancellationRequested)
                {
                    // Генерируем случайное число с учетом текущего состояния
                    double increment = _random.NextDouble() * 0.1; // Базовое увеличение (от 0 до 0.1)

                    // Проверка на "паровоз"
                    if (_random.NextDouble() < _trainProbability)
                    {
                        increment *= _random.Next(5, 15); // Большое увеличение
                    }

                    // Увеличение скорости роста числа по мере продолжительности игры
                    increment *= (1 + currentNumber / _maxThreshold);

                    currentNumber += increment;

                    // Ограничиваем число в диапазоне от 1.3 до 3.5
                    currentNumber = Math.Clamp(currentNumber, 1.3, _maxThreshold);

                    // Отправляем текущее число и ускорение клиентам
                    await hubContext.Clients.All.SendAsync("ReceiveNumber", Math.Round(currentNumber, 2), increment, cancellationToken);

                    logger.LogDebug($"Sent number to clients: {Math.Round(currentNumber, 2)}, increment: {increment}");

                    // Вычисляем шанс краха, пропорциональный текущему числу
                    double crashChance = (currentNumber - 1.3) / (_maxThreshold - 1.3);

                    // Учитываем общую вероятность победы
                    crashChance *= (1 - _globalWinProbability);

                    // Проверка на крах
                    if (_random.NextDouble() < crashChance)
                    {
                        await hubContext.Clients.All.SendAsync("Crush", Math.Round(currentNumber, 2), cancellationToken);
                        logger.LogWarning($"Game ended due to a crash. Value: {Math.Round(currentNumber, 2)}");
                        _isRunning = false;
                    }

                    // Время раунда пропорционально числу краха
                    int roundTime = (int)(_maxRoundTime - (currentNumber / _maxThreshold) * (_maxRoundTime - _minRoundTime));
                    roundTime = Math.Max(roundTime, _minRoundTime); // Ограничиваем минимальное время раунда

                    // Задержка для имитации времени раунда
                    await Task.Delay(roundTime, cancellationToken);
                }

                if (!_isRunning)
                {
                    logger.LogInformation("Game stopped.");
                }
            }
        }

        public void StopGame()
        {
            if (_isRunning)
            {
                _isRunning = false;
                logger.LogInformation("Game stopped manually.");
            }
            else
            {
                logger.LogWarning("Attempted to stop an already stopped game.");
            }
        }

        public void SetGlobalWinProbability(double probability)
        {
            if (probability < 0 || probability > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(probability), "Probability must be between 0 and 1.");
            }

            _globalWinProbability = probability;
            logger.LogInformation($"Global win probability set to {probability * 100}%.");
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Освобождаем управляемые ресурсы
                    StopGame();
                    logger.LogInformation("Game disposed.");
                }

                // Освобождаем неуправляемые ресурсы (если есть)
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Game()
        {
            Dispose(false);
        }
    }
}