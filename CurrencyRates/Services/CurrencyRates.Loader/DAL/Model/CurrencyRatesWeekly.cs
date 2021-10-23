using System;

namespace CurrencyRates.Loader.DAL.Model
{
    /// <summary>
    /// Rates of currency during each week (needed to build a report)
    /// </summary>
    public class CurrencyRatesWeekly
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Day of the week when the currency is calculated 
        /// </summary>
        public DateTime CurrencyDate { get; set; }

        /// <summary>
        /// Provider identifier
        /// </summary>
        public int ProviderId { get; set; }

        /// <summary>
        /// Provider of currency
        /// </summary>
        public virtual Provider Provider { get; set; }

        /// <summary>
        /// Minimum rate for particular currency
        /// </summary>
        public decimal MinRatesPerWeek { get; set; }

        /// <summary>
        /// Maximum rate for particular currency
        /// </summary>
        public decimal MaxRatesPerWeek { get; set; }

        /// <summary>
        /// Base currency to compare 
        /// </summary>
        public int CurrencyBaseId { get; set; }

        /// <summary>
        /// Type of specific currency (type of currency) for conversion (Rates)
        /// </summary>
        public virtual Currency CurrencyBase { get; set; }

        /// <summary>
        /// Currency identifier
        /// </summary>
        public int CurrencyId { get; set; }

        /// <summary>
        /// Currency information
        /// </summary>
        public virtual Currency Currency { get; set; }
    }
}