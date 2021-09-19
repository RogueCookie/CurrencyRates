using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CurrencyRates.Core.Enums;
using CurrencyRates.Core.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace CurrencyRates.CzBank.Connector.Services
{
    /// <summary>
    /// Service for send data to the Loader service
    /// </summary>
    public class JobRegistrationService : IHostedService
    {
        private readonly RabbitSettings _settings;
        private readonly AddNewJobModel _registerSettings;
        private readonly ILogger<JobRegistrationService> _logger;

        public JobRegistrationService(IOptions<RabbitSettings> options, IOptions<AddNewJobModel> registerSettings, ILogger<JobRegistrationService> logger)
        {
            _settings = options.Value ?? throw new ArgumentNullException(nameof(options));
            _registerSettings = registerSettings.Value ?? throw new ArgumentNullException(nameof(registerSettings));
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            DeclareChannel();
            return Task.CompletedTask;
        }


        /// <summary>
        /// Setup connection to RabbitMQ server
        /// </summary>
        public void DeclareChannel()
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = _settings.HostName,
                    UserName = _settings.Login,
                    Password = _settings.Password,
                    Port = _settings.Port
                };

                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();
                RegistrationInScheduler(channel);
            }
            catch (BrokerUnreachableException ex)
            {
                _logger.LogError(ex, "RabbitService Client DeclareChannel() error in Czech Bank connector");
            }
        }

        /// <summary>
        /// Self registration for current service in Scheduler if not exist
        /// </summary>
        private void RegistrationInScheduler(IModel channel)
        {
            var modelForRegistration = _registerSettings;
            var message = JsonConvert.SerializeObject(modelForRegistration);
            var messageToBytes = Encoding.UTF8.GetBytes(message);

            channel.ExchangeDeclare(exchange: Exchanges.Scheduler.ToString(), type: ExchangeType.Direct);

            var properties = channel.CreateBasicProperties();
            properties.CorrelationId = Guid.NewGuid().ToString();

            channel.BasicPublish(
                exchange:Exchanges.Scheduler.ToString(),
                routingKey: RoutingKeys.AddNewJob.ToString(),
                basicProperties: properties,
                body: messageToBytes);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}