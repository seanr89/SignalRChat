using Domain;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ChatConsole
{
    class Program
    {
        private static IConfigurationRoot? Configuration { get; set; }
        private static string? _userName { get; set; }
        private static bool _history = false;
        static async Task Main(string[] args)
        {
            Console.WriteLine("SignalR Chatter\r");
            Console.WriteLine("------------------------\n");

            //configure the default host for local (current work IP)
            //string host = "http://10.15.38.39:5000/chatHub";
            //Home
            string host = "http://localhost:3000/chatHub";

            //Ask the user if they want to configure a different host
            Console.WriteLine("Enter host (i.e. http://localhost:3000/chatHub - or leave blank and use default!)");
            var inputHost = Console.ReadLine();
            //Check if there was anything provided - otherwise we use the default as preconfigured!
            if (!string.IsNullOrWhiteSpace(inputHost))
                host = inputHost;
                
            Console.WriteLine("Enter a UserName!");
            _userName = Console.ReadLine();

            try
            {
                var connection = new HubConnectionBuilder().WithUrl(host).Build();
                await connection.StartAsync().ContinueWith(task =>
                {
                    if (task.IsFaulted)
                        Console.WriteLine("There was an error opening the connection:{0}", task.Exception.GetBaseException());
                    else
                        Console.WriteLine("Connected");
                });
                //Registers a handler that will be invoked when the connection is closed.
                connection.Closed+= (error) => {
                    // Do your close logic here!
                    return Task.CompletedTask;
                };

                Console.WriteLine("Press Ctrl-C to close.");
                var cts = new CancellationTokenSource();
                Console.CancelKeyPress += async (sender, a) =>
                {
                    a.Cancel = true;
                    cts.Cancel();
                    await connection.InvokeAsync("sendMessage", "ConsoleClient", $"{_userName} has left");
                    await connection.StopAsync();
                    await connection.DisposeAsync();
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

                //Connection and request chat history - may only need this once
                connection.On("broadcastHistory", (string message) =>
                {
                    if(_history)
                        return;
                    var records = JsonConvert.DeserializeObject<IEnumerable<ChatRecord>>(message) ?? new List<ChatRecord>();
                    if(records.Any()){
                        Console.WriteLine($"Chat history received");
                        //Console.WriteLine($"Found a total of {records.Count()} recs with first value: {records.ToList()[0].ToString()}");
                        foreach (var item in records)
                        {
                            Console.WriteLine($"{item.username} says: {item.message}");
                        }
                    }
                    _history = true;
                });

                //Send Connection Message
                await connection.InvokeAsync("sendMessage", "ConsoleClient", $"{_userName} has connected");
                //Make request to get history
                await connection.InvokeAsync("getChatHistory");

                Console.WriteLine("Please write into chat below!");
                while (true)
                {
                    int currentCursorLine = Console.CursorTop;
                    // wait for user to write something into the chat
                    string content = Console.ReadLine() ?? String.Empty;
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
                //TODO add connection disposal here!
            }
        }
    }
}
