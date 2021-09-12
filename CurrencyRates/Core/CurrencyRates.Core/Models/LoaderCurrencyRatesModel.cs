using System;
using System.Collections.Generic;
using CurrencyRates.Core.Enums;

namespace CurrencyRates.Core.Models
{
    /// <summary>
    /// Currency rate model for one of the currencies. Allow to serialize/deserialize data when we get them from the clients
    /// </summary>
    public class LoaderCurrencyRatesModel
    {
        /// <summary>
        /// Date when was getting data about rates
        /// </summary>
        public DateTime ActualDateTime { get; set; }

        /// <summary>
        /// List of rates (Kind of currency (shortcut) and it's rates)
        /// </summary>
        public List<DailyRates> Rates { get; set; }

        /// <summary>
        /// Currency by which the rest of the rates are correlated 
        /// </summary>
        public TypeOfCurrency MasterCurrency { get; set; }
    }
}