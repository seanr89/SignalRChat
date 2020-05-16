using System;

namespace SignalServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Signal R Chatter Host in C#\r");
            Console.WriteLine("------------------------\n");

            string url = "http://127.0.0.1:8088/";
            var server = new Server(url);

            // Map the default hub url (/signalr)
            server.MapHubs();

            // Start the server
            server.Start();

            Console.WriteLine("Server running on {0}", url);

            // Keep going until somebody hits 'x'
            while (true) {
                ConsoleKeyInfo ki = Console.ReadKey(true);
                if (ki.Key == ConsoleKey.X) {
                    break;
                }
            }
        }
    }
}
