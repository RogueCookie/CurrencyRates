using System.Collections.Generic;

namespace CurrencyRates.Core.Models
{
    /// <summary>
    /// Currency rates in specific time range
    /// </summary>
    public class TimedCurrencyRatesModel : BaseRabbitMessage
    {
        /// <summary>
        /// List of currency in particular date range
        /// </summary>
        public List<LoaderCurrencyRatesModel> TimedRates { get; set; }
    }
}