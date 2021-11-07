using System;
using CurrencyRates.Loader.DAL.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace CurrencyRates.Loader.DAL.Repositories
{
    public class CurrencyDailyRepository : ICurrencyDailyRepository
    {
        private readonly LoaderContext _loaderContext;
        private readonly ILogger<CurrencyDailyRepository> _logger;

        public CurrencyDailyRepository(LoaderContext loaderContext, ILogger<CurrencyDailyRepository> logger)
        {
            _loaderContext = loaderContext;
            _logger = logger;
        }
        
        /// <inheritdoc /> 
        public async Task StoreRatesDailyAsync([NotNull] IEnumerable<CurrencyRatesDaily> currencyRatesModel)
        {
            try
            {
                await _loaderContext.CurrencyRatesDailies
                    .AddRangeAsync(currencyRatesModel);
                await _loaderContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred during storing daily rates", ex);
            }
        }
    }
}