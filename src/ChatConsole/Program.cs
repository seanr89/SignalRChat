using System;
using System.Threading;
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

            Console.WriteLine("Enter your name");    
            string name = Console.ReadLine();

            var connection = new HubConnectionBuilder().WithUrl("http://localhost:5000/chatHub").Build();

            await connection.StartAsync();
            Console.WriteLine("Starting connection. Press Ctrl-C to close.");
            var cts = new CancellationTokenSource();
            Console.CancelKeyPress += async (sender, a) =>
            {
                Console.WriteLine("Cancel Pressed");
                a.Cancel = true;
                cts.Cancel();
                await connection.InvokeAsync("sendMessage", "ConsoleClient", $"{name} has left");
                Environment.Exit(0);
            };

            //var mes = Console.ReadLine();
            connection.On("broadcastMessage", (string userName, string message) => {
                Console.WriteLine($"{userName} says: {message}");
            });

            await connection.InvokeAsync("sendMessage", "ConsoleClient", $"{name} has connected");

            // Console.WriteLine("Press ESC to stop");
            while(true)
            {
                // do something   
                string content = Console.ReadLine();
                if(!string.IsNullOrWhiteSpace(content))
                    await connection.InvokeAsync("sendMessage", $"{name}:", content);
            }
            //Console.WriteLine("Client is shutting down...");
        }
    }
}
