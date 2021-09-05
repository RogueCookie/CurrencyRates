using System;
using System.Threading.Tasks;
using Xunit;

namespace CurrencyRates.CzBAnk.ConnectorTests
{
    public class ClientConnectorServiceTest
    {
        [Fact]
        public async Task DownloadDailyRates_ListOfRatesExpected()
        {
            var currentDate = DateTime.Now;
            var service = HttpClientHelpers.CreateClintConnectorService();
            var result = await service.DownloadDataDailyAsync(currentDate);

            Assert.NotNull(result);
        }
    }
}