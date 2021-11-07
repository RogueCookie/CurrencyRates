using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CurrencyRates.Core.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CurrencyRates.Loader.MediatR.Queries
{
    public class ValidateResponseModel : IRequest<TimedCurrencyRatesModel>
    {
        /// <summary>
        /// Data from queue. What come
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Id of job for end-to-end logging
        /// </summary>
        public string CorrelationId { get; set; }
    }

    public class ValidateResponseQueryHandler : IRequestHandler<ValidateResponseModel, TimedCurrencyRatesModel>
    {
        private readonly ILogger _logger;

        /// <summary>
        /// used for canary deploy 
        /// </summary>
        private readonly List<string> _availableMessageVersions = new() { "1.0", "0.1", "0.3"};

        public ValidateResponseQueryHandler(ILogger<ValidateResponseModel> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public Task<TimedCurrencyRatesModel> Handle(ValidateResponseModel request, CancellationToken cancellationToken)
        {
            if (request.Message == null)
            {
                _logger.LogError($"{request.Message} cannot be null", request.CorrelationId);
                throw new ArgumentNullException($"{request.Message} cannot be null", request.CorrelationId);
            }

            try
            {
                var deserializeModel = JsonConvert.DeserializeObject<BaseRabbitMessage>(request.Message);

                if (!_availableMessageVersions.Contains(deserializeModel?.Version))
                {
                    _logger.LogWarning($"Unsupported version of message {deserializeModel?.Version} from {deserializeModel?.SourceName}", request.CorrelationId);
                    throw new ArgumentNullException($"Unsupported version of message {deserializeModel?.Version} from {deserializeModel?.SourceName}", request.CorrelationId);
                }

                var timeRatesModel = JsonConvert.DeserializeObject<TimedCurrencyRatesModel>(request.Message);
                return Task.FromResult(timeRatesModel);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception,"Unable to deserialize message at exception", request.CorrelationId);
                throw; 
            }
        }
    }
}