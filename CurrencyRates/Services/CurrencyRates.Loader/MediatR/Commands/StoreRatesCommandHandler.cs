using CurrencyRates.Core.Models;
using CurrencyRates.Loader.DAL.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CurrencyRates.Loader.DAL.Model;

namespace CurrencyRates.Loader.MediatR.Commands
{
    /// <summary>
    /// Save data rates into db
    /// </summary>
    public class StoreRates : TimedCurrencyRatesModel, IRequest
    {
    }

    public class StoreRatesCommandHandler : IRequestHandler<StoreRates>
    {
        private readonly ICurrencyDailyRepository _currencyDailyRepository;
        private readonly IProviderRepository _providerRepository;
        private readonly ILogger<StoreRatesCommandHandler> _logger;

        public StoreRatesCommandHandler(ICurrencyDailyRepository currencyDailyRepository, IProviderRepository providerRepository, ILogger<StoreRatesCommandHandler> logger)
        {
            _currencyDailyRepository = currencyDailyRepository;
            _providerRepository = providerRepository;
            _logger = logger;
        }


        public async Task<Unit> Handle(StoreRates request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Storing rates for daily begins {sourceName} with count {count}", request.SourceName, request.TimedRates.Count);

            var provider = await _providerRepository.GetProviderByNameAsync(request.SourceName);
            if (provider == null)
            {
                _logger.LogError("Provider cannot be null");
                throw new ArgumentNullException();
            }

            foreach (var timeRate in request.TimedRates)
            {
                var mappedRates = timeRate.Rates.Select(x => new CurrencyRatesDaily()
                {
                    CurrencyId = (int) x.CurrencyType,
                    CurrencyBaseId = (int) timeRate.MasterCurrency,
                    DateTime = timeRate.ActualDateTime,
                    ProviderId =  provider.Id,
                    CurrencyRate = x.Rate
                }).ToList();

                _logger.LogInformation("Mapped rates for storing data {sourceName} with count {count}", request.SourceName, mappedRates.Count());

                await _currencyDailyRepository.StoreRatesDailyAsync(mappedRates);
            }

            return await Unit.Task;
        }
    }
}