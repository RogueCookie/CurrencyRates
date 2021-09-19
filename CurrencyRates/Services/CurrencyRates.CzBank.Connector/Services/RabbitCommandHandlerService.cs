using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CurrencyRates.Core.Enums;
using CurrencyRates.Core.Models;
using CurrencyRates.CzBank.Connector.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CurrencyRates.CzBank.Connector.Services
{
    /// <summary>
    /// Service for handling all messages from RabbitMq
    /// </summary>
    public class RabbitCommandHandlerService : BackgroundService
    {
        private readonly RabbitSettings _options;
        private readonly AddNewJobModel _registerSettings;
        private readonly ILogger<RabbitCommandHandlerService> _logger;
        private readonly IClientConnectorService _clientConnectorService;
        private readonly IDataCommandSender _commandSender;
        private IConnection _connection;
        private IModel _channel;

        public RabbitCommandHandlerService(IOptions<RabbitSettings> options,
            IOptions<AddNewJobModel> registerSettings,
            ILogger<RabbitCommandHandlerService> logger,
            IClientConnectorService clientConnectorService,
            IDataCommandSender commandSender)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            _registerSettings = registerSettings.Value ?? throw new ArgumentNullException(nameof(registerSettings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _clientConnectorService = clientConnectorService ?? throw new ArgumentNullException(nameof(clientConnectorService));
            _commandSender = commandSender ?? throw new ArgumentNullException(nameof(commandSender));
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            InitializeRabbitMQListener();
            return Task.CompletedTask;
            
        }

        // ReSharper disable once InconsistentNaming
        /// <summary>
        /// Through one queue, the Scheduler will send a message (command) which work must be executed 
        /// </summary>
        private void InitializeRabbitMQListener()
        {
            var factory = new ConnectionFactory
            {
                HostName = _options.HostName,
                UserName = _options.Login,
                Password = _options.Password,
                Port = _options.Port,
                DispatchConsumersAsync = true
            };

            _connection = factory.CreateConnection(clientProvidedName: "Cz Bank Connector listener");
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(Exchanges.Scheduler.ToString(), ExchangeType.Direct);

            var queueName = "Execute.Job.CzBank";
            _channel.QueueDeclare(queueName, exclusive: false, durable: true, autoDelete: false);
            _channel.BasicQos(0, 1, false);
            _channel.QueueBind(queueName, Exchanges.Scheduler.ToString(), _registerSettings.RoutingKey);

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += async (model, args) =>
            {
                var body = args.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
                var commandModel = JsonConvert.DeserializeObject<AddNewJobModel>(message);
                if (commandModel == null)
                {
                    _logger.LogError($"Cannot deserialized model for message {message}", args.BasicProperties.CorrelationId);
                    return;
                }
                commandModel.CorrelationId = args.BasicProperties.CorrelationId;

                await ExecuteCommand(commandModel);
                _logger.LogInformation($"Consumer {queueName} with mes {message}", args.BasicProperties.CorrelationId);
            };
            _channel.BasicConsume(queueName, consumer: consumer, autoAck: false);
        }

        /// <summary>
        /// Switching between incoming commands 
        /// </summary>
        /// <param name="command">Type of command for execution</param>
        private async Task ExecuteCommand(AddNewJobModel command)
        {
            switch (command.Command)
            {
                case "Download":
                    var currencyRatesResponse = await _clientConnectorService.DownloadDataDailyAsync(DateTime.UtcNow, command.CorrelationId);
                    _commandSender.SendDataToLoader(currencyRatesResponse, command.CorrelationId); 
                    break;
                case "StoreDate":
                    Store();
                    break;
                default:
                    throw new Exception();
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        private void Store()
        {
            Console.WriteLine("Store");
        }

        /// <summary>
        /// TODO
        /// </summary>
        private void Download()
        {
            Console.WriteLine("Download");
        }
    }
}