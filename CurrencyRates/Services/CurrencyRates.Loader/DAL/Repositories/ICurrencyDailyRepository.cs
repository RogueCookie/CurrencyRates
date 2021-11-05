using CurrencyRates.Loader.DAL.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyRates.Loader.DAL.Repositories
{
    /// <summary>
    /// Repository for manipulation with currency daily table
    /// </summary>
    public interface ICurrencyDailyRepository
    {
        /// <summary>
        /// Add or update daily currency rates in db
        /// </summary>
        /// <param name="currencyRatesModel">List of daily rates</param>
        Task StoreRatesDailyAsync(IEnumerable<CurrencyRatesDaily> currencyRatesModel);
    }
}