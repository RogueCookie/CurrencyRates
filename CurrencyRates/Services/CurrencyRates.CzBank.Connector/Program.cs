using System.Threading;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using CurrencyRates.Core;
using CurrencyRates.CzBank.Connector.Models;
using CurrencyRates.CzBank.Connector.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace CurrencyRates.CzBank.Connector
{
    internal class Program
    {
        private static IConfiguration _configuration;

        static async Task Main(string[] args)
        {
            using var host = new ServiceHost(args);

            host.ConfigureServices((builderContext, services) =>
            {
                _configuration = builderContext.Configuration;

                services.Configure<RabbitSettings>(_configuration.GetSection("RabbitSettings"));
                services.Configure<AddNewJobModel>(_configuration.GetSection("RegisterSettings"));

                services.AddHostedService<RabbitCommandHandlerService>();

                var logger = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .CreateLogger();

                services.AddLogging(loggingBuilder =>
                    loggingBuilder.AddSerilog(logger));
            },
                (services) =>
                {
                    return new AutofacServiceProviderFactory((container) =>
                    {
                        container.Populate(services);
                    });
                });

            // execute ones when service starts
            await host.RunAsync((serviceProvider) =>
            {

            });
        }
    }
}
