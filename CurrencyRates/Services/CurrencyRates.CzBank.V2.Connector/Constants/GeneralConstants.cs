namespace CurrencyRates.CzBank.V2.Connector.Constants
{
    /// <summary>
    /// Constants used in CzBankConnector
    /// </summary>
    public class GeneralConstants
    {
        /// <summary>
        /// Name for the base http client
        /// </summary>
        public const string BaseCzBankUri = "baseCzUri";

        /// <summary>
        /// Url for creating full daily url (additional to base)
        /// </summary>
        public const string AdditionalUrlDaily = "en/financial_markets/foreign_exchange_market/exchange_rate_fixing/daily.txt?date";

        /// <summary>
        /// Url for creating full yearly url (additional to base)
        /// </summary>
        public const string AdditionalUrlYearly = "en/financial_markets/foreign_exchange_market/exchange_rate_fixing/year.txt?year";

        /// <summary>
        /// Source name it's provider
        /// </summary>
        public const string SourceName = "Czech Bank";

        /// <summary>
        /// Version of service
        /// </summary>
        public const string Version = "1.0";
    }
}