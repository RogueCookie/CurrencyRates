using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using CurrencyRates.Core.Enums;
using CurrencyRates.Core.Models;
using CurrencyRates.CzBank.Connector.Extensions;
using CurrencyRates.CzBank.Connector.Interfaces;
using CurrencyRates.CzBank.Connector.Models;
using Microsoft.Extensions.Logging;
using DailyRates = CurrencyRates.CzBank.Connector.Models.DailyRates;

namespace CurrencyRates.CzBank.Connector.Services
{
    /// <summary>
    /// Services for getting data from the client (Currency rates from Czech Bank)
    /// </summary>
    public class ClientRatesRatesConnectorService : IClientRatesConnectorService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ClientRatesRatesConnectorService> _logger;

        public ClientRatesRatesConnectorService(IHttpClientFactory httpClientFactory, ILogger<ClientRatesRatesConnectorService> logger)
        {
            // take free client from the factory
            _httpClient = httpClientFactory.CreateClient(Constants.GeneralConstants.BaseCzBankUri);
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<TimedCurrencyRatesModel> DownloadDataDailyAsync(DateTime date, string correlationId, CancellationToken cancellationToken)
        {
            if (date > DateTime.Now)
            {
                _logger.LogError("Received data from future is not allowed, parameter: {date}", date, correlationId);
                throw new ArgumentNullException($"Received data from future is not allowed, parametr: {date}");
            }

            try
            {
                var response = await _httpClient.GetStringAsync($"{Constants.GeneralConstants.AdditionalUrlDaily}={date:dd.MM.YYYY}", cancellationToken);

                TextReader textReader = new StringReader(response);

                //allow to clean first row with wrong data
                await textReader.ReadLineAsync();

                _logger.LogInformation("Received data from provider at {DateTime.Now} with message response {response}", DateTime.Now, response, correlationId);

                var csvReader = new CsvReader(textReader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    Delimiter = "|"
                });

                var responseModel = csvReader.GetRecords<DailyRates>().ToList();
                var convertResponse = responseModel.ConvertResponse(date, TypeOfCurrency.CZH); 

                return convertResponse; 
            }
            catch (Exception exception)
            {
                _logger.LogError(exception,"Czech Bank Connector Request error", correlationId);
                throw;
            }
        }

        public async Task<TimedCurrencyRatesModel> DownloadDataYearlyAsync(DateTime year, string correlationId, CancellationToken cancellationToken)
        {
            if (year.Year > DateTime.Now.Year)
            {
                _logger.LogError("Received data from future is not allowed, parameter: {date}", year.Year, correlationId);
                throw new ArgumentNullException($"Received data from future is not allowed, parametr: {year.Year}");
            }

            var result = new TimedCurrencyRatesModel()
            {
                TimedRates = new List<LoaderCurrencyRatesModel>(),
                Version = Constants.GeneralConstants.Version,
                SourceName = Constants.GeneralConstants.SourceName
            };

            try
            {
                var response = await _httpClient.GetStringAsync($"{Constants.GeneralConstants.AdditionalUrlYearly}={year.Year}", cancellationToken);

                TextReader textReader = new StringReader(response);
                var header = await textReader.ReadLineAsync();

                var columns = GetColumnName(header);
                
                result = await CollectTimedRatesModel(textReader, columns, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Unable to parse data from the client for yearly rates for year = {year.Year}", year.Year, correlationId);
                throw;
            }

            return result;
        }

        /// <summary>
        /// Parse first row from textReader for getting column name except First column (date)
        /// </summary>
        /// <param name="header">First row from text reader</param>
        /// <returns>Array of CurrencyAmount which contains column name in particular order</returns>
        private static CurrencyAmount[] GetColumnName(string header)
        {
            return header.Split('|').Skip(1).Select(x => new CurrencyAmount()
            {
                Amount = int.Parse(x.Split(' ').First()),
                Code = x.Split(' ').Last()
            }).ToArray();
        }

        /// <summary>
        /// Collect model for subsequent processing
        /// </summary>
        /// <param name="textReader">Data from the client</param>
        /// <param name="columns">Column name in particular order</param>
        /// <param name="currencyRatesModel">Initialized model</param>
        /// <returns>Filled (collected) model</returns>
        private async Task<TimedCurrencyRatesModel> CollectTimedRatesModel(TextReader textReader, CurrencyAmount[] columns, TimedCurrencyRatesModel currencyRatesModel)
        {
            string line;
            while ((line = await textReader.ReadLineAsync()) != null)
            {
                var row = line.Split('|');

                var date = DateTime.ParseExact(row.First(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
                var currencyRates = row.Skip(1).ToArray();

                var ratesDaily = new List<DailyRates>();

                for (var i = 0; i < columns?.Length; i++)
                {
                    ratesDaily.Add(new DailyRates()
                    {
                        Amount = columns[i].Amount,
                        Code = columns[i].Code,
                        Rate = decimal.Parse(currencyRates[i])
                    });
                }
                currencyRatesModel.TimedRates.AddRange(ratesDaily.ConvertResponseYearly(date, TypeOfCurrency.CZH));
            }

            return currencyRatesModel;
        }
    }
}