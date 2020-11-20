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

            string host = "http://10.15.38.39:5000/chatHub"; //http://localhost:5000/chatHub

            Console.WriteLine("Enter a host");
            var inputHost = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(inputHost))
                host = inputHost;
            else
                Console.WriteLine($"Using host: {host}");

            Console.WriteLine("Enter your name");
            string name = Console.ReadLine();

            var connection = new HubConnectionBuilder().WithUrl(host).Build();

            await connection.StartAsync();
            Console.WriteLine("Starting connection. Press Ctrl-C to close.");

            //Handle cancellation/closure events
            var cts = new CancellationTokenSource();
            Console.CancelKeyPress += async (sender, a) =>
            {
                a.Cancel = true;
                cts.Cancel();
                await connection.InvokeAsync("sendMessage", "ConsoleClient", $"{name} has left");
                Environment.Exit(0);
            };

            //Listen for incoming messages from signalR hub
            connection.On("broadcastMessage", (string userName, string message) =>
            {
                Console.WriteLine($"{userName} says: {message}");
            });

            await connection.InvokeAsync("sendMessage", "ConsoleClient", $"{name} has connected");

            Console.WriteLine("Please write into chat below!");
            while (true)
            {
                // wait for user to write something into the chat
                string content = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(content))
                    await connection.InvokeAsync("sendMessage", $"{name}:", content);
            }
        }
    }
}
