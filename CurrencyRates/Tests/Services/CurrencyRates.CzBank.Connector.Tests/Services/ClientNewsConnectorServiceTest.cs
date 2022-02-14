using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CurrencyRates.CzBank.Connector.Services;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;

namespace CurrencyRates.CzBank.Connector.Tests.Services
{
    public class ClientNewsConnectorServiceTest
    {
        [Test]
        public async Task DownloadRssNewsAsync()
        {
            var token = new CancellationToken();
            var service = new ClientNewsConnectorService(new NullLogger<ClientNewsConnectorService>()); 
            service.DownloadRssNewsAsync(token);
        }
    }
}