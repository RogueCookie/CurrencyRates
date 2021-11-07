namespace CurrencyRates.CzBank.Connector.Models
{
    /// <summary>
    /// Model for parse data from the client
    /// </summary>
    public class CurrencyAmount
    {
        /// <summary>
        /// Code of currency
        /// <example>AUD</example>
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Amount of currency
        /// </summary>
        public int Amount { get; set; }
    }
}