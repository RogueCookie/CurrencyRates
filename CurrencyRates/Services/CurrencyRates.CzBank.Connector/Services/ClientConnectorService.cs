using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using CurrencyRates.Core.Enums;
using CurrencyRates.Core.Models;
using CurrencyRates.CzBank.Connector.Constants;
using CurrencyRates.CzBank.Connector.Extensions;
using CurrencyRates.CzBank.Connector.Interfaces;
using Microsoft.Extensions.Logging;
using DailyRates = CurrencyRates.CzBank.Connector.Models.DailyRates;

namespace CurrencyRates.CzBank.Connector.Services
{
    /// <summary>
    /// Services for getting data from the client (Currency rates from Czech Bank)
    /// </summary>
    public class ClientConnectorService : IClientConnectorService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ClientConnectorService> _logger;

        public ClientConnectorService(IHttpClientFactory httpClientFactory, ILogger<ClientConnectorService> logger)
        {
            // take free client from the factory
            _httpClient = httpClientFactory.CreateClient(HttpClientConstants.Daily);
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc />
        public async Task<TimedCurrencyRatesModel> DownloadDataDailyAsync(DateTime date)
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"{HttpClientConstants.AdditionalUrl}={date:dd.MM.YYYY}");

                TextReader textReader = new StringReader(response);

                var dailyRequestData = await textReader.ReadLineAsync();

                _logger.LogInformation($"Received data from provider at {DateTime.Now}");

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
                _logger.LogError("Czech Bank Connector Request error {Message}", exception.Message);
                throw;
            }
        }
    }
}