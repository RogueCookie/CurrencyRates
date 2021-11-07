namespace CurrencyRates.CzBank.Connector.Models
{
    /// <summary>
    /// Model for getting data from the Czech Bank client
    /// </summary>
    public class DailyRates : CurrencyAmount
    {
        /// <summary>
        /// Country of currency
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Text description of currency 
        /// <example>dollar</example>
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Current rate
        /// </summary>
        public decimal Rate { get; set; }
    }
}