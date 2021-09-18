using System;
using System.Threading.Tasks;
using CurrencyRates.Core.Models;

namespace CurrencyRates.CzBank.Connector.Interfaces
{
    public interface IClientConnectorService
    {
        /// <summary>
        /// Download currency rates from particular source on current date
        /// </summary>
        /// <returns>List of rates in time range with info about Provider (data serialized from source)</returns>
        Task<TimedCurrencyRatesModel> DownloadDataDailyAsync(DateTime date);
    }
}