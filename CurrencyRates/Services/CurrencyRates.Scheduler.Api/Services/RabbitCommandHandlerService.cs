using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CurrencyRates.Core.Enums;
using CurrencyRates.Core.Models;
using CurrencyRates.Scheduler.Api.MediatR.Commands;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CurrencyRates.Scheduler.Api.Services
{
    /// <summary>
    /// Service for handle queues
    /// </summary>
    public class RabbitCommandHandlerService : BackgroundService
    {
        private readonly string _hostname;
        private readonly int _port;
        private readonly string _username;
        private readonly string _password;
        private readonly IMediator _mediator;
        private IConnection _connection;
        private IModel _channel;
        private readonly ILogger<RabbitCommandHandlerService> _logger;

        public RabbitCommandHandlerService(IOptions<RabbitSettings> options, IMediator mediator, ILogger<RabbitCommandHandlerService> logger)
        {
            _hostname = options.Value.HostName;
            _port = options.Value.Port;
            _username = options.Value.Login;
            _password = options.Value.Password;
            _mediator = mediator;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            InitializeRabbitMQListener();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Get the message to scheduler command
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnReceived(object sender, BasicDeliverEventArgs e)
        {
            var body = e.Body;
            var message = Encoding.UTF8.GetString(body.ToArray());
            try
            {
                _logger.LogInformation($"Scheduler consume {e.RoutingKey} Received {message} with correlation_id {e.BasicProperties.CorrelationId}");
                var commandModel = JsonConvert.DeserializeObject<AddNewJobModel>(message);
                _mediator.Send(new AddNewJob()
                {
                    JobName = commandModel.JobName,
                    RoutingKey = commandModel.RoutingKey,
                    Version = commandModel.Version,
                    Command = commandModel.Command,
                    CronScheduler = commandModel.CronScheduler,
                    IsEnabled = commandModel.IsEnabled,
                    CorrelationId = e.BasicProperties.CorrelationId
                });
                _channel?.BasicAck(e.DeliveryTag, false);
            }
            catch (Exception exception)
            {
                _logger.LogError("Error on received", exception);
            }
        }


        // ReSharper disable once InconsistentNaming
        /// <summary>
        /// Through one queue, the Scheduler will receive a message that some connectors have appeared and he needs to add a job 
        /// </summary>
        private void InitializeRabbitMQListener()
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostname,
                UserName = _username,
                Password = _password,
                Port = _port
            };

            _connection = factory.CreateConnection(clientProvidedName: "Scheduler listener");
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(Exchanges.Scheduler.ToString(), ExchangeType.Direct);

            var queueName = "Register.New.Job";
            _channel.QueueDeclare(queueName, exclusive: false, durable: true, autoDelete: false);
            _channel.BasicQos(0, 1, false);
            _channel.QueueBind(queueName, Exchanges.Scheduler.ToString(), RoutingKeys.AddNewJob.ToString());

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += OnReceived;
            _channel.BasicConsume(queueName, consumer: consumer, autoAck: false);
            _logger.LogInformation("Scheduler registrations is Ok");
        }
    }
}