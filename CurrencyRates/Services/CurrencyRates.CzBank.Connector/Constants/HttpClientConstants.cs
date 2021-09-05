namespace CurrencyRates.CzBank.Connector.Constants
{
    /// <summary>
    /// Constants used in CzBankConnector
    /// </summary>
    public class HttpClientConstants
    {
        /// <summary>
        /// Name for the http client
        /// </summary>
        public const string Daily = "daily";

        /// <summary>
        /// Url for creating full url ( additional to base)
        /// </summary>
        public const string AdditionalUrl = "en/financial_markets/foreign_exchange_market/exchange_rate_fixing/daily.txt?date";
    }
}