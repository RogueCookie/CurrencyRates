using CurrencyRates.Core.Enums;

namespace CurrencyRates.Core.Models
{
    /// <summary>
    /// Model for getting currency rates daily
    /// </summary>
    public class DailyRates
    {
        /// <summary>
        /// Amount of specific currency (type of currency) for conversion (Rates)
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Shortcut of currency
        /// </summary>
        public TypeOfCurrency CurrencyType { get; set; }

        /// <summary>
        /// The ratio of crowns to the amount 
        /// </summary>
        public decimal Rate { get; set; }
    }
}