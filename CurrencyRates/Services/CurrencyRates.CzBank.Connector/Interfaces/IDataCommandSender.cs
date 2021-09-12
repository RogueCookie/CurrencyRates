using System.Threading.Tasks;
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
        /// <returns></returns>//TODO
        Task SendDataToLoader(TimedCurrencyRatesModel timedCurrencyRatesModel);
    }
}