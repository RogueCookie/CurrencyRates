using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace CurrencyRates.Scheduler.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Allow to read parameters appsettings.json when app starts and if .env have the same variable as in appsettings then they were
        /// overwritten with parameters that are passed to .env file
        /// This is how we configure the application without changing the upsets physically on the disk
        /// </summary>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseContentRoot(Directory.GetCurrentDirectory())
                        .ConfigureAppConfiguration((hostingContext, config) =>
                        {
                            var env = hostingContext.HostingEnvironment;
                            // fluent configuration
                            // configure service settings  
                            config
                                // set the base path as the current execution path 
                                .SetBasePath(env.ContentRootPath)
                                // add particular json file
                                .AddJsonFile("appsettings.json", true, true)
                                // reads the configuration value from the environment variable (.env)
                                .AddEnvironmentVariables();
                        });
                    webBuilder.UseStartup<Startup>().UseDefaultServiceProvider(options =>
                        options.ValidateScopes = false);
                });
    }
}
