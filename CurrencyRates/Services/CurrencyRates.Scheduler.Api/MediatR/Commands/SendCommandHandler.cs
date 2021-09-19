﻿using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CurrencyRates.Core.Enums;
using CurrencyRates.Core.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace CurrencyRates.Scheduler.Api.MediatR.Commands
{
    public class SendCommand : AddNewJobModel, IRequest
    {
    }

    public class SendCommandHandler : IRequestHandler<SendCommand>
    {
        private readonly ILogger<SendCommandHandler> _logger;
        private readonly RabbitSettings _settingOptions;

        public SendCommandHandler(IOptions<RabbitSettings> options, ILogger<SendCommandHandler> logger)
        {
            _logger = logger;
            _settingOptions = options.Value;
        }

        /// <summary>
        /// Send command by mediator
        /// </summary>
        /// <param name="request">Kind of command for being send</param>
        /// <param name="cancellationToken">Token for stopping execution</param>
        public async Task<Unit> Handle(SendCommand request, CancellationToken cancellationToken)
        {
            var message = JsonConvert.SerializeObject(request);
            var messageBytes = Encoding.UTF8.GetBytes(message);

            var factory = new ConnectionFactory
            {
                HostName = _settingOptions.HostName,
                UserName = _settingOptions.Login,
                Password = _settingOptions.Password,
                Port = _settingOptions.Port
            };

            using var connection = factory.CreateConnection(clientProvidedName: "Scheduler send command");
            var channel = connection.CreateModel();
            channel.ExchangeDeclare(exchange: Exchanges.Scheduler.ToString(), type: ExchangeType.Direct);

            var properties = channel.CreateBasicProperties();
            properties.CorrelationId = request.CorrelationId;

            _logger.LogInformation($"Data send to Scheduler {message}", request.CorrelationId);

            channel.BasicPublish(
                exchange: Exchanges.Scheduler.ToString(),
                routingKey: request.RoutingKey,
                basicProperties: properties,
                body: messageBytes);

            channel.Close();
            connection.Close();

            return await Unit.Task;
        }
    }
}