using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using CurrencyRates.CzBank.Connector.Constants;
using CurrencyRates.CzBank.Connector.Interfaces;
using CurrencyRates.CzBank.Connector.Models;
using Microsoft.Extensions.Logging;

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
        public async Task<List<DailyRates>> DownloadDataDailyAsync(DateTime date)
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"{HttpClientConstants.AdditionalUrl}={date:dd.MM.YYYY}");

                TextReader textReader = new StringReader(response);

                var dailyRequestData = await textReader.ReadLineAsync();

                _logger.LogInformation(dailyRequestData);

                var csvReader = new CsvReader(textReader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    Delimiter = "|"
                });

                return csvReader.GetRecords<DailyRates>().ToList();

            }
            catch (Exception exception)
            {
                _logger.LogError("Czech Bank Connector Request error {Message}", exception.Message);
                return null;
            }
        }
    }
}