namespace CurrencyRates.Core.Models
{
    public class BaseRabbitMessage
    {
        /// <summary>
        /// Name of source from where downloaded current rates
        /// </summary>
        public string SourceName { get; set; }

        /// <summary>
        /// Message version (Canary deploy) 
        /// </summary>
        public string Version { get; set; }
    }
}