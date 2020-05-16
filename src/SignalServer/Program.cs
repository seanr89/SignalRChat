using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace SignalServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Signal R Chatter Host in C#\r");
            Console.WriteLine("------------------------\n");

            CreateWebHostBuilder(args).Build().Run();
        }

            private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
                WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
        }
    }
