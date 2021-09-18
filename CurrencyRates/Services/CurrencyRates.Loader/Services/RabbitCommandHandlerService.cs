﻿using CurrencyRates.Core.Enums;
using CurrencyRates.Core.Models;
using CurrencyRates.Loader.MediatR.Queries;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CurrencyRates.Loader.Services
{
    public class RabbitCommandHandlerService : BackgroundService
    {
        private readonly IMediator _mediator;
        private readonly ILogger<RabbitCommandHandlerService> _logger;
        private readonly RabbitSettings _settings;
        private IConnection _connection;
        private IModel _channel;

        public RabbitCommandHandlerService(IOptions<RabbitSettings> options, IMediator mediator, ILogger<RabbitCommandHandlerService> logger)
        {
            _mediator = mediator;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _settings = options.Value ?? throw new ArgumentNullException(nameof(options));
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        { 
            DeclareChannel();

            if (_channel != null)
                await Consume(cancellationToken);
            else
            {
                //helthcheck все плохо перезапусти сервис
            }
        }


        /// <summary>
        /// Setup connection to RabbitMQ server
        /// </summary>
        private void DeclareChannel()
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

                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

            }
            catch (BrokerUnreachableException ex)
            {
                // apply retry logic helthcheck TODO все плохо перезапусти сервис
            }
        }

        /// <summary>
        /// Read message from the queue 
        /// </summary>
        private async Task Consume(CancellationToken cancellationToken)
        {
            if (_channel == null) throw new ArgumentNullException(nameof(_channel));

            _channel.ExchangeDeclare(Exchanges.Loader.ToString(), ExchangeType.Direct, true);

            var queues = _channel.QueueDeclare();
            _channel.QueueBind(queues.QueueName, Exchanges.Loader.ToString(), "");

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += async (model, args) =>
            {
                var body = args.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
                _logger.LogInformation($"");//TODO

                var result = await _mediator.Send(new ValidateResponseModel()
                {
                    Message = message
                }, cancellationToken);

                if (result == null)
                {
                    //dead letter queue //TODO
                    return;
                }
            };
            _channel.BasicConsume(queues.QueueName, true, consumer);
        }
    }
}