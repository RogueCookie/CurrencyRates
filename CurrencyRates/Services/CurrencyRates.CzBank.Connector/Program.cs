using System;
using System.Threading.Tasks;
using CurrencyRates.Core;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace CurrencyRates.CzBank.Connector
{
    internal class Program
    {
        private static IConfiguration _configuration;

        static async Task Main(string[] args)
        {
            using var host = new ServiceHost(args);

            await host.RunAsync((serviceProvider) =>
            {
                var baseInit = _configuration.GetValue<bool?>("BaseInit");
                Log.Information($"Base Init - {baseInit}");
                if (baseInit.HasValue && baseInit.Value)
                {
                    try
                    {
                        //var dbInit = serviceProvider.GetRequiredService<DbInitializator>();
                        //dbInit.Initialize().GetAwaiter().GetResult();
                    }
                    catch (Exception exception)
                    {
                        Log.Error(exception.Message);
                    }
                }
                //var eventBus = serviceProvider.GetRequiredService<RabbitMQClient>();
                //eventBus.Start();
            });
        }
    }
}
