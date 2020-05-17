using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ChatConsole
{
    class Program
    {
        //useful: https://github.com/aspnet/SignalR-samples
        private static IConfigurationRoot Configuration { get; set; }
        static async Task Main(string[] args)
        {
            // Display title as the C# SignalR Chat
            Console.WriteLine("Signal R Chatter in C#\r");
            Console.WriteLine("------------------------\n");

            //Console.WriteLine("Enter your name");    
            //string name = Console.ReadLine();

            var connection = new HubConnectionBuilder().WithUrl("http://localhost:5000/chatHub").Build();

            await connection.StartAsync();

            //var mes = Console.ReadLine();
            connection.On("broadcastMessage", (string userName, string message) => {
                Console.WriteLine($"{userName} says: {message}");
            });

            await connection.InvokeAsync("sendMessage", "Sean", "Hello");

            Console.WriteLine("Message Sent");

            // Console.WriteLine("Press ESC to stop");
            // while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
            // {
            //     // do something
            // }
            Console.WriteLine("Waiting for response");
            Console.ReadKey();
            Console.WriteLine("Client is shutting down...");
        }
        
        /// <summary>
        /// Query app settings json content
        /// </summary>
        /// <returns></returns>
        private static IConfigurationRoot LoadAppSettings()
        {
             Console.WriteLine("LoadAppSettings");
            try
            {
                var config = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

                return config;
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("Error trying to load app settings");
                return null;
            }
        }
    }
}
