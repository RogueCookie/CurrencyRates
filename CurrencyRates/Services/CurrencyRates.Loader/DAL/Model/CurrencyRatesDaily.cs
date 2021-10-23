using System;

namespace CurrencyRates.Loader.DAL.Model
{
    /// <summary>
    /// Data about currencies rates per day
    /// </summary>
    public class CurrencyRatesDaily
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Base currency to compare 
        /// </summary>
        public int CurrencyBaseId { get; set; }

        /// <summary>
        /// Type of specific currency (type of currency) for conversion (Rates)
        /// </summary>
        public virtual Currency CurrencyBase { get; set; }

        /// <summary>
        /// Rate for particular currency
        /// </summary>
        public decimal CurrencyRate { get; set; }

        /// <summary>
        /// Date with time when it was downloaded
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Provider identifier
        /// </summary>
        public int ProviderId { get; set; }

        /// <summary>
        /// Provider of currency
        /// </summary>
        public virtual Provider Provider { get; set; }

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