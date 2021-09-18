using CurrencyRates.Core.Enums;
using CurrencyRates.Core.Models;
using CurrencyRates.CzBank.Connector.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Text;
using Newtonsoft.Json;

namespace CurrencyRates.CzBank.Connector.Services
{
    public class DataCommandSender : IDataCommandSender
    {
        private readonly RabbitSettings _options;
        private IConnection _connection;
        private IModel _channel;
        private readonly ILogger<RabbitCommandHandlerService> _logger;

        public DataCommandSender(IOptions<RabbitSettings> options, ILogger<RabbitCommandHandlerService> logger)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        ///<inheritdoc />
        public void SendDataToLoader(TimedCurrencyRatesModel timedCurrencyRatesModel)
        {
            var factory = new ConnectionFactory
            {
                HostName = _options.HostName,
                UserName = _options.Login,
                Password = _options.Password,
                Port = _options.Port,
                DispatchConsumersAsync = true
            };
            _connection = factory.CreateConnection(clientProvidedName: "Cz Bank Connector publisher");
            _channel = _connection.CreateModel();

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            Publish(timedCurrencyRatesModel);
        }

        /// <summary>
        /// Send data to Exchange Loader
        /// </summary>
        /// <param name="model">Common AMQP model</param>
        /// <param name="timedCurrencyRatesModel">Data of currency rates in some time range</param>
        private void Publish(TimedCurrencyRatesModel timedCurrencyRatesModel)
        {
            try
            {
                var serializedData = JsonConvert.SerializeObject(timedCurrencyRatesModel);
                var dataInBytes = Encoding.UTF8.GetBytes(serializedData);
                _channel.BasicPublish(exchange: Exchanges.Loader.ToString(), routingKey: "", basicProperties: null, body: dataInBytes);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An exception occurred during attempt to publish message to exchange Loader {ex} at {DateTime.Now}");
            }
            
        }
    }
}