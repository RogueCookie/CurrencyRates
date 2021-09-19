using CurrencyRates.Core.Models;

namespace CurrencyRates.CzBank.Connector.Interfaces
{
    /// <summary>
    /// Command for sending data
    /// </summary>
    public interface IDataCommandSender
    {
        /// <summary>
        /// Send data to Exchange Loader
        /// </summary>
        /// <param name="timedCurrencyRatesModel">Prepared data from the client</param>
        /// <param name="correlationId">Id of particular job for end-to-end logging</param>
        void SendDataToLoader(TimedCurrencyRatesModel timedCurrencyRatesModel, string correlationId);
    }
}