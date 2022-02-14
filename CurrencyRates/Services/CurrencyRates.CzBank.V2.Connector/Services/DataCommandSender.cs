using System;
using System.Text;
using CurrencyRates.Core.Enums;
using CurrencyRates.Core.Models;
using CurrencyRates.CzBank.V2.Connector.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace CurrencyRates.CzBank.V2.Connector.Services
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
        public void SendDataToLoader(TimedCurrencyRatesModel timedCurrencyRatesModel, string correlationId)
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
            Publish(timedCurrencyRatesModel, correlationId);
        }

        /// <summary>
        /// Send data to Exchange Loader
        /// </summary>
        /// <param name="timedCurrencyRatesModel">Data of currency rates in some time range</param>
        /// <param name="correlationId"></param>
        private void Publish(TimedCurrencyRatesModel timedCurrencyRatesModel, string correlationId)
        {
            try
            {
                var serializedData = JsonConvert.SerializeObject(timedCurrencyRatesModel);
                var dataInBytes = Encoding.UTF8.GetBytes(serializedData);
                var properties = _channel.CreateBasicProperties();
                properties.CorrelationId = correlationId;
                _channel.BasicPublish(exchange: Exchanges.Loader.ToString(), routingKey: "", basicProperties: properties, body: dataInBytes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,$"An exception occurred during attempt to publish message to exchange Loader", correlationId);
            }
            
        }
    }
}