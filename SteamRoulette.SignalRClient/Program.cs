using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace SteamRoulette.Client
{
    class Program
    {
        private static HubConnection _connection;

        static async Task Main(string[] args)
        {
            Console.WriteLine("Connecting to the game hub...");

            // Создаем подключение к GameHub
            _connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7069/gameHub") // URL вашего GameHub
                .Build();

            // Подписываемся на события от сервера
            _connection.On<double, double>("ReceiveNumber", (number, increment) =>
            {
                Console.WriteLine($"Current number: {number}, increment: {increment}");
            });

            _connection.On<double>("Crush", (crashValue) =>
            {
                Console.WriteLine($"Game crushed at: {crashValue}");
            });

            // Подключаемся к серверу
            try
            {
                await _connection.StartAsync();
                Console.WriteLine("Connected to the game hub!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to connect to the game hub: {ex.Message}");
                return;
            }

            // Основной цикл ожидания
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

            // Отключаемся от сервера
            await _connection.StopAsync();
            Console.WriteLine("Disconnected from the game hub.");
        }
    }
}