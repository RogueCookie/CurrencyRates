using System;
using CurrencyRates.CzBank.Connector.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CurrencyRates.CzBank.Connector.Services
{
    public class RabbitMqService
    {
        private readonly RabbitSettings _settings;
        private readonly ILogger<RabbitMqService> _logger;

        public RabbitMqService(IOptions<RabbitSettings> options, ILogger<RabbitMqService> logger)
        {
            _settings = options.Value ?? throw new ArgumentNullException(nameof(options));
            _logger = logger;
        }

        public void Start()
        {
            _logger.LogInformation($"host name = {_settings.HostName}, port = {_settings.Port},  login = {_settings.Login}. password = {_settings.Password}");
        }
    }
}