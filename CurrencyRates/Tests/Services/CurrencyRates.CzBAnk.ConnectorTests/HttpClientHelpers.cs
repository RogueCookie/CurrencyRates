using CurrencyRates.CzBank.Connector.Interfaces;
using Moq;
using System;
using System.Net.Http;
using CurrencyRates.CzBank.Connector.Services;
using Microsoft.Extensions.Logging.Abstractions;

namespace CurrencyRates.CzBAnk.ConnectorTests
{
    public class HttpClientHelpers
    {
        public static IClientConnectorService CreateClintConnectorService()
        {
            var mockHttpFactory = new Mock<IHttpClientFactory>();
            var client = new HttpClient()
            {
                BaseAddress = new Uri("http://www.cnb.cz")
            };
            mockHttpFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);

            return new ClientConnectorService(mockHttpFactory.Object, new NullLogger<ClientConnectorService>());
        }
    }
}