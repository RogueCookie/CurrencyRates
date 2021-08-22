using System;
using System.Text;
using CurrencyRates.CzBank.Connector.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace CurrencyRates.CzBank.Connector.Services
{
    public class RabbitMqService
    {
        private readonly RabbitSettings _settings;
        private readonly ILogger<RabbitMqService> _logger;
        private const string ROUTING_KEY = "connectorToLoader";

        public RabbitMqService(IOptions<RabbitSettings> options, ILogger<RabbitMqService> logger)
        {
            _settings = options.Value ?? throw new ArgumentNullException(nameof(options));
            _logger = logger;
        }

        public void Start()
        {
            _logger.LogInformation($"host name = {_settings.HostName}, port = {_settings.Port},  login = {_settings.Login}. password = {_settings.Password}");
            DeclareChannel();
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
                Publish(channel);
            }
            catch (BrokerUnreachableException ex)
            {
                Console.WriteLine(ex.InnerException);
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.Data.Keys);
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// Prepare and send message to the exchange
        /// </summary>
        private static void Publish(IModel channel)
        {
            if (channel == null) throw new ArgumentNullException(nameof(channel));

            channel.ExchangeDeclare(Exchanges.Loader.ToString(), ExchangeType.Direct, true);

            while (true)
            {
                var message = "hello, from Cz bank connector";
                var newBody = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(Exchanges.Loader.ToString(), ROUTING_KEY, null, newBody);
            }
        }

    }
}