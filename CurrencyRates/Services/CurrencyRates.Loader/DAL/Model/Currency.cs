using System.Collections.Generic;

namespace CurrencyRates.Loader.DAL.Model
{
    /// <summary>
    /// All available type of currencies
    /// </summary>
    public class Currency
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Shortcut of currency
        /// </summary>
        public string CurrencyAlias { get; set; }

        public virtual IEnumerable<CurrencyRatesDaily> CurrencyRatesDailies { get; set; }

        public virtual IEnumerable<CurrencyRatesWeekly> CurrencyRatesWeeklies { get; set; }
    }
}