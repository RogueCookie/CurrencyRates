using System.Collections.Generic;

namespace CurrencyRates.Core.Models
{
    public class TimedCurrencyRatesModel
    {
        /// <summary>
        /// List of currency in particular date range
        /// </summary>
        public List<LoaderCurrencyRatesModel> TimedRates { get; set; }

        /// <summary>
        /// Name of source from where downloaded current rates
        /// </summary>
        public string SourceName { get; set; }

        /// <summary>
        /// Версия сообщения Канареечный деплой
        /// </summary>
        public string Version { get; set; }
    }
}