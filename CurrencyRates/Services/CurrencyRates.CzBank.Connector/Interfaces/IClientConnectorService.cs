using System;
using System.Threading.Tasks;
using CurrencyRates.Core.Models;

namespace CurrencyRates.CzBank.Connector.Interfaces
{
    /// <summary>
    /// Get data from the Czech bank
    /// </summary>
    public interface IClientConnectorService
    {
        /// <summary>
        /// Download currency rates from particular source on current date
        /// </summary>
        /// <param name="date"></param>
        /// <param name="correlationId">Id of particular job for end-to-end logging</param>
        /// <returns>List of rates in time range with info about Provider(data serialized from source)</returns>
        Task<TimedCurrencyRatesModel> DownloadDataDailyAsync(DateTime date, string correlationId);
    }
}