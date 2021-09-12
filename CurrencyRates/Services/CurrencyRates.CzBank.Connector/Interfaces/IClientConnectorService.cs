﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CurrencyRates.Core.Models;

namespace CurrencyRates.CzBank.Connector.Interfaces
{
    public interface IClientConnectorService
    {
        /// <summary>
        /// Download currency rates from particular source on current date
        /// </summary>
        /// <returns>List of clientModels (data serialized from source)</returns>
        Task<List<LoaderCurrencyRatesModel>> DownloadDataDailyAsync(DateTime date);
    }
}