using System.Collections.Generic;

namespace CurrencyRates.Loader.DAL.Model
{
    /// <summary>
    /// Data about all available providers
    /// </summary>
    public class Provider
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Provider name
        /// </summary>
        public string ProviderName { get; set; }

        /// <summary>
        /// Additional description of provider
        /// </summary>
        public string Description { get; set; }

        public virtual IEnumerable<CurrencyRatesDaily> RatesDaily { get; set; }

        public virtual IEnumerable<CurrencyRatesWeekly> RatesWeekly { get; set; }
    }
}