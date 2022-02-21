
using Moq;
using System;
using System.Net.Http;
using CurrencyRates.CzBank.V2.Connector.Interfaces;
using Microsoft.Extensions.Logging.Abstractions;
using CurrencyRates.CzBank.V2.Connector.Services;

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