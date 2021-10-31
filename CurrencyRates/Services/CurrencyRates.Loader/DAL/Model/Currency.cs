using CurrencyRates.Core.Enums;
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
        public TypeOfCurrency Alias { get; set; }

        /// <summary>
        /// Country of the currency
        /// </summary>
        public string OriginalCountry { get; set; }

        public virtual IEnumerable<CurrencyRatesDaily> RatesDaily { get; set; }

        public virtual IEnumerable<CurrencyRatesWeekly> RatesWeekly { get; set; }

        public virtual IEnumerable<CurrencyRatesDaily> BaseRatesDaily { get; set; }

        public virtual IEnumerable<CurrencyRatesWeekly> BaseRatesWeekly { get; set; }
    }
}