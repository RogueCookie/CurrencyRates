using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using CurrencyRates.Core;
using CurrencyRates.Core.Models;
using CurrencyRates.CzBank.Connector.Constants;
using CurrencyRates.CzBank.Connector.Interfaces;
using CurrencyRates.CzBank.Connector.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
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
                
                services.AddHostedService<JobRegistrationService>();
                services.AddTransient<IClientConnectorService, ClientConnectorService>();
                services.AddHostedService<RabbitCommandHandlerService>();

                services.AddHttpClient(HttpClientConstants.Daily, client =>
                {
                    client.BaseAddress = new Uri("https://www.cnb.cz");
                });//.AddPolicyHandler(GetRetryPolicy()); TODO

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

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            throw new NotImplementedException();
        }
    }
}
