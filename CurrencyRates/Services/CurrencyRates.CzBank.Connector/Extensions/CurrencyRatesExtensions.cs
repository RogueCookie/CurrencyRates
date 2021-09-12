using System;
using System.Collections.Generic;
using System.Linq;
using CurrencyRates.Core.Enums;
using CurrencyRates.Core.Models;
using CurrencyRates.CzBank.Connector.Models;
using DailyRates = CurrencyRates.Core.Models.DailyRates;

namespace CurrencyRates.CzBank.Connector.Extentions
{
    /// <summary>
    /// Methods for preparation data before sending to another service (Loader)
    /// </summary>
    public static class CurrencyRatesExtensions
    {
        /// <summary>
        /// Convert data from the client to our principal model
        /// </summary>
        /// <param name="dailyRates">Data got from the client</param>
        /// <param name="dateTime">Date when data was downloaded</param>
        /// <param name="masterCurrency">The currency by which the downloaded currency rates are compared</param>
        /// <returns>Prepared model for sending to Loader service</returns>
        public static LoaderCurrencyRatesModel ConvertResponse(this IEnumerable<Models.DailyRates> dailyRates, DateTime dateTime, TypeOfCurrency masterCurrency)
        {
            return new LoaderCurrencyRatesModel()
            {
                ActualDateTime = dateTime,
                MasterCurrency = masterCurrency,
                Rates = dailyRates
                    .Where(y => Enum.TryParse(typeof(TypeOfCurrency), y.Code, out var __))
                    .Select(x => new DailyRates()
                    {
                        Amount = x.Amount,
                        CurrencyType = (TypeOfCurrency)Enum.Parse(typeof(TypeOfCurrency), x.Code, true),
                        Rate = x.Rate
                    })
                    .ToList()
            };
        }
    }
}