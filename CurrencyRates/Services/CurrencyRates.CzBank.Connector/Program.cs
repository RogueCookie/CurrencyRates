using Autofac.Extensions.DependencyInjection;
using CurrencyRates.Core;
using CurrencyRates.Core.Models;
using CurrencyRates.CzBank.Connector.Interfaces;
using CurrencyRates.CzBank.Connector.Services;
using CurrencyRates.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;
using System.Threading.Tasks;

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
                services.AddTransient<IClientRatesConnectorService, ClientRatesRatesConnectorService>();
                services.AddTransient<IDataCommandSender, DataCommandSender>();
                services.AddHostedService<RabbitCommandHandlerService>();

                services.AddHttpClient(Constants.GeneralConstants.BaseCzBankUri, client =>
                {
                    client.BaseAddress = new Uri("https://www.cnb.cz");
                }).AddPolicyHandler(GetRetryPolicy());

                //services.AddSerilogLogging(_configuration);
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
        /// Add policy for retry HTTP requests (for HttpClientFactory resilience and transient-fault handling capabilities).
        /// Handle HttpRequestExceptions, 404 not found and 5xx status codes.
        /// </summary>
        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}
