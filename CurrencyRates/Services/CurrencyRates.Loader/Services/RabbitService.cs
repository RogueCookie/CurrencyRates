﻿using System;
using System.Text;
using CurrencyRates.Loader.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace CurrencyRates.Loader.Services
{
    public class RabbitService
    {
        private readonly ILogger<RabbitService> _logger;
        private readonly RabbitSettings _settings;
        private const string ROUTING_KEY = "connectorToLoader";

        public RabbitService(IOptions<RabbitSettings> options, ILogger<RabbitService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _settings = options.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public void Start()
        {
            _logger.LogInformation($"hostName = { _settings.HostName}, port = { _settings.Port}, login = { _settings.Login}. password = { _settings.Password}");
            DeclareChannel();
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

                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();
                Consume(channel);
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
        /// Read message from the queue 
        /// </summary>
        private static void Consume(IModel channel)
        {
            if (channel == null) throw new ArgumentNullException(nameof(channel));

            channel.ExchangeDeclare(Exchanges.Loader.ToString(), ExchangeType.Direct, true);

            var queues = channel.QueueDeclare();
            channel.QueueBind(queues.QueueName, Exchanges.Loader.ToString(), ROUTING_KEY);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, args) =>
            {
                var body = args.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
                Console.WriteLine($"We got message at {DateTime.Now}  with txt {message}");
            };
            channel.BasicConsume(queues.QueueName, true, consumer);
            Console.WriteLine("Press me, please");
            Console.ReadLine();
        }
    }
}