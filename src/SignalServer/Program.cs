using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace SignalServer;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Signal R Chatter Host\r");
        Console.WriteLine("------------------------\n");

        CreateWebHostBuilder(args).Build().Run();
    }

    private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
        .UseUrls("http://hostname:3000;https://hostname:3001")
        .UseStartup<Startup>();
}