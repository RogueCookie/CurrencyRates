using System;
using System.Collections.Generic;
using System.Linq;
using CurrencyRates.Core.Enums;
using CurrencyRates.Core.Models;
using DailyRates = CurrencyRates.Core.Models.DailyRates;

namespace CurrencyRates.CzBank.Connector.Extensions
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
        public static List<LoaderCurrencyRatesModel> ConvertResponse(this IEnumerable<Models.DailyRates> dailyRates, DateTime dateTime, TypeOfCurrency masterCurrency)
        {
            return new List<LoaderCurrencyRatesModel>()
            {
                new LoaderCurrencyRatesModel()
                {
                    ActualDateTime = dateTime,
                    MasterCurrency = masterCurrency,
                    Rates = dailyRates
                        .Where(y => Enum.TryParse(typeof(TypeOfCurrency), y.Code, out var __))
                        .Select(x => new DailyRates()
                        {
                            Amount = x.Amount,
                            CurrencyType = (TypeOfCurrency) Enum.Parse(typeof(TypeOfCurrency), x.Code, true),
                            Rate = x.Rate
                        })
                        .ToList()
                }
            };
        }
    }
}