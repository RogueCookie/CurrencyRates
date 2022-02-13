using System;

namespace CurrencyRates.CzBank.Connector.Models
{
    /// <summary>
    /// Short list of new available on Cz Bank
    /// </summary>
    public class RssModel
    {
        /// <summary>
        /// Title for the new
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Short description for news
        /// </summary>
        public string ShortDescription { get; set; }

        /// <summary>
        /// Link for getting full description of news
        /// </summary>
        public string LinkForFullDescription { get; set; }

        /// <summary>
        /// Date when news was published
        /// </summary>
        public DateTimeOffset PublishDate { get; set; }
    }
}