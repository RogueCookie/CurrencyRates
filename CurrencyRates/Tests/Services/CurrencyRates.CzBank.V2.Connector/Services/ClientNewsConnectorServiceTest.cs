using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CurrencyRates.CzBank.Grpc.Rss;
using CurrencyRates.CzBank.V2.Connector.Services;
using Grpc.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using NUnit.Framework;

namespace CurrencyRates.CzBank.V2.Connector.Tests.Services
{
    public class ClientNewsConnectorServiceTest : Grpc.Rss.RssService.RssServiceBase
    {
        private ServerCallContext _serviceCallContext;

        [SetUp]
        public void Setup()
        {
            
            //var myConfiguration = new Dictionary<string, string>
            //{
            //    {"NewsUrl", "https://www.cnb.cz/en/.content/rss-feed/rss-feed_tz.xml"}
            //};

            //var configuration = new ConfigurationBuilder()
            //    .AddInMemoryCollection(myConfiguration)
            //    .Build();

             _serviceCallContext = new Mock<ServerCallContext>().Object;
        }

        [Test]
        public async Task CheckSerialization_WhenDownloadNews_ResultExpected()
        {
            var mockFactory = new Mock<IHttpClientFactory>();
            var client = new HttpClient()
            {
                BaseAddress = new Uri("https://www.cnb.cz")
            };
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            var service = new ClientNewsConnectorService(mockFactory.Object);

            var request = new GetInfoRequest();
            var response = await service.Download(request, _serviceCallContext);
            Assert.True(response.Items.Count > 5);
        }

        [Test]
        public async Task CheckBrokenConnection_WhenWrongXmlFile_FailedExpected()
        {
            var mockFactory = new Mock<IHttpClientFactory>();
            var handlerMock = new Mock<HttpMessageHandler>();

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("the string you want to return")
                })
                .Verifiable();
            var client = new HttpClient(handlerMock.Object);
            client.BaseAddress = new Uri("http://google.com");
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            var service = new ClientNewsConnectorService(mockFactory.Object);

            var request = new GetInfoRequest();
            var response = await service.Download(request, _serviceCallContext);
            Assert.Null(response);
        }
    }
}