namespace CurrencyRates.Core.Enums
{
    /// <summary>
    /// Contains all routing keys for connection to rabbit
    /// </summary>
    public enum RoutingKeys
    {
        /// <summary>
        /// For registration new job in scheduler
        /// </summary>
        AddNewJob = 1
    }
}