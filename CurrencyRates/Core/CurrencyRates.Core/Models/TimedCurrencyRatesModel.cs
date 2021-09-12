using System.Collections.Generic;

namespace CurrencyRates.Core.Models
{
    /// <summary>
    /// Currency rates in specific time range
    /// </summary>
    public class TimedCurrencyRatesModel
    {
        /// <summary>
        /// List of currency in particular date range
        /// </summary>
        public List<LoaderCurrencyRatesModel> TimedRates { get; set; }

        /// <summary>
        /// Name of source from where downloaded current rates
        /// </summary>
        public string SourceName { get; set; }

        /// <summary>
        /// Message version (Canary deploy) 
        /// </summary>
        public string Version { get; set; }
    }
}