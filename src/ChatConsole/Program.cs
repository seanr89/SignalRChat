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
        //https://docs.microsoft.com/en-us/aspnet/core/signalr/dotnet-client?view=aspnetcore-5.0&tabs=visual-studio
        private static IConfigurationRoot Configuration { get; set; }
        private static string _userName { get; set; }
        static async Task Main(string[] args)
        {
            // Display title as the C# SignalR Chat
            Console.WriteLine("SignalR Chatter\r");
            Console.WriteLine("------------------------\n");

            //configure the default host for local (current work IP)
            //string host = "http://10.15.38.39:5000/chatHub";
            //Home
            string host = "http://localhost:5000/chatHub";

            //Ask the user if they want to configure a different host
            Console.WriteLine("Enter host (i.e. http://localhost:5000/chatHub - or leave blank and use default!)");
            var inputHost = Console.ReadLine();
            //Check if there was anything provided - otherwise we use the default as preconfigured!
            if (!string.IsNullOrWhiteSpace(inputHost))
                host = inputHost;

            Console.WriteLine($"Using host: {host}");
            Console.WriteLine("Enter a UserName!");
            _userName = Console.ReadLine();

            //TODO: include a username check!

            try
            {
                var connection = new HubConnectionBuilder().WithUrl(host).Build();

                await connection.StartAsync();
                Console.WriteLine("Starting connection. Press Ctrl-C to close.");

                //Handle cancellation/closure events
                var cts = new CancellationTokenSource();
                Console.CancelKeyPress += async (sender, a) =>
                {
                    a.Cancel = true;
                    cts.Cancel();
                    await connection.InvokeAsync("sendMessage", "ConsoleClient", $"{_userName} has left");
                    Environment.Exit(0);
                };

                //Listen for incoming messages from signalR hub
                connection.On("broadcastMessage", (string userName, string message) =>
                {
                    if (userName == _userName)
                        Console.WriteLine($"You said: {message}");
                    else
                        Console.WriteLine($"{userName} says: {message}");
                });

                await connection.InvokeAsync("sendMessage", "ConsoleClient", $"{_userName} has connected");

                Console.WriteLine("Please write into chat below!");
                while (true)
                {
                    int currentCursorLine = Console.CursorTop;
                    // wait for user to write something into the chat
                    string content = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(content))
                    {
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        await connection.InvokeAsync("sendMessage", $"{_userName}", ": " + content);
                    }
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine($"Caught exception {e.Message}");
                throw;
            }
            finally
            {
                Console.WriteLine("Closing App");
            }
        }
    }
}
