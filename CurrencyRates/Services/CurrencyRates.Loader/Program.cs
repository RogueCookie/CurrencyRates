using System;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using CurrencyRates.Core;
using CurrencyRates.Loader.Models;
using CurrencyRates.Loader.Services;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

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

                    services.Configure<RabbitSettings>(_configuration.GetSection("RabbitSettings"));
                    services.AddSingleton<RabbitService>();

                    // Configure serilog. to create the serilog logger, based on the configuration provided in appsettings.json
                    // provides a fluent interface for building a logging pipeline
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

            await host.RunAsync((serviceProvider) =>
            {
                var eventBus = serviceProvider.GetRequiredService<RabbitService>();
                eventBus.Start();
            });
        }
    }
}
