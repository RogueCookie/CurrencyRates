using Autofac.Extensions.DependencyInjection;
using CurrencyRates.Core;
using CurrencyRates.Core.Models;
using CurrencyRates.Loader.Services;
using CurrencyRates.Logging;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Threading.Tasks;

namespace CurrencyRates.Loader
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

                   // services.AddHostedService<MigrationInitService>();
                    services.Configure<RabbitSettings>(_configuration.GetSection("RabbitSettings"));
                    services.AddHostedService<RabbitCommandHandlerService>();
                    services.AddMediatR(Assembly.GetExecutingAssembly());

                    // Configure serilog. to create the serilog logger, based on the configuration provided in appsettings.json
                    // provides a fluent interface for building a logging pipeline
                    services.AddSerilogLogging(_configuration);
                },
                (services) =>
                {
                    return new AutofacServiceProviderFactory((container) =>
                    {
                        container.Populate(services);
                    });
                });

            await host.RunAsync((serviceProvider) => { });
        }
    }
}
