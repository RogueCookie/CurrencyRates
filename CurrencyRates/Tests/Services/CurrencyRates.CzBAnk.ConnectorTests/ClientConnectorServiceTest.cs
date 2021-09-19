using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CurrencyRates.CzBAnk.ConnectorTests
{
    public class ClientConnectorServiceTest
    {
        [Fact]
        public async Task DownloadDailyRates_ModelWithListOfRatesExpected()
        {
            var currentDateUtc = DateTime.UtcNow;
            var service = HttpClientHelpers.CreateClintConnectorService();
            var result = await service.DownloadDataDailyAsync(currentDateUtc, Guid.NewGuid().ToString());
            var dateFromResult = result.TimedRates.FirstOrDefault()?.ActualDateTime;

            Assert.NotNull(result);
            Assert.NotNull(dateFromResult);
            Assert.Equal(currentDateUtc, dateFromResult);
        }
    }
}