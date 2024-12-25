using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SteamRoulette.WebApi
{
    public class GameHub : Hub
    {
        private readonly Game _game;
        private readonly ILogger<GameHub> _logger;

        public GameHub(Game game, ILogger<GameHub> logger)
        {
            _game = game;
            _logger = logger;
        }

        /// <summary>
        /// Переопределяем метод OnConnectedAsync для обработки подключения клиента.
        /// </summary>
        public override Task OnConnectedAsync()
        {
            _logger.LogInformation($"Client connected: {Context.ConnectionId}");
            return base.OnConnectedAsync();
        }

        /// <summary>
        /// Переопределяем метод OnDisconnectedAsync для обработки отключения клиента.
        /// </summary>
        public override Task OnDisconnectedAsync(Exception exception)
        {
            _logger.LogInformation($"Client disconnected: {Context.ConnectionId}");
            return base.OnDisconnectedAsync(exception);
        }
    }
}