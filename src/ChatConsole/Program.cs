using System;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ChatConsole
{
    class Program
    {

        private static IConfigurationRoot Configuration { get; set; }
        static void Main(string[] args)
        {
            // Display title as the C# SignalR Chat
            Console.WriteLine("Signal R Chatter in C#\r");
            Console.WriteLine("------------------------\n");

            var connection = new HubConnectionBuilder().WithUrl("http://localhost:7071/api").Build();

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

        /// <summary>
        /// Prep/Configure Dependency Injection
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        private static void RegisterAndInjectServices(IServiceCollection services, IConfiguration config)
        {
            Console.WriteLine("RegisterAndInjectServices");

            services.AddLogging(logging =>
            {
                logging.AddConsole();
            }).Configure<LoggerFilterOptions>(options=>options.MinLevel=                         
                                                LogLevel.Warning);
        }
    }
}
