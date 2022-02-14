using CurrencyRates.CzBank.Connector.Interfaces;
using CurrencyRates.CzBank.Connector.Services;
using Moq;
using System;
using System.Net.Http;
using Microsoft.Extensions.Logging.Abstractions;

namespace CurrencyRates.CzBank.Connector.Tests
{
    public class HttpClientHelpers
    {
        public static IClientRatesConnectorService CreateClintConnectorService()
        {
            var mockHttpFactory = new Mock<IHttpClientFactory>();
            var client = new HttpClient()
            {
                BaseAddress = new Uri("http://www.cnb.cz")
            };
            mockHttpFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);

            return new ClientRatesRatesConnectorService(mockHttpFactory.Object, new NullLogger<ClientRatesRatesConnectorService>());
        }
    }
}