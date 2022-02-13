using System;
using System.Threading;
using System.Threading.Tasks;
using CurrencyRates.Core.Models;

namespace CurrencyRates.CzBank.Connector.Interfaces
{
    /// <summary>
    /// Get rates data from the Czech bank
    /// </summary>
    public interface IClientRatesConnectorService
    {
        /// <summary>
        /// Download currency rates from particular source on current date
        /// </summary>
        /// <param name="date">Date for getting currencies</param>
        /// <param name="correlationId">Id of particular job for end-to-end logging</param>
        /// <returns>List of rates in time range with info about Provider(data serialized from source)</returns>
        Task<TimedCurrencyRatesModel> DownloadDataDailyAsync(DateTime date, string correlationId, CancellationToken cancellationToken);

        /// <summary>
        /// Download currency rates from particular source on current year
        /// </summary>
        /// <param name="year">Year from which to get currencies</param>
        /// <param name="correlationId">Id of particular job for end-to-end logging</param>
        /// <returns>List of rates in time range with info about Provider(data serialized from source)</returns>
        Task<TimedCurrencyRatesModel> DownloadDataYearlyAsync(DateTime year, string correlationId, CancellationToken cancellationToken);
    }
}