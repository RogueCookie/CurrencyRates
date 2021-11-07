using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyRates.CzBank.Connector.Tests
{
    public class ClientConnectorServiceTest
    {
        [Test]
        public async Task DownloadDailyRates_ModelWithListOfRatesExpected()
        {
            var currentDateUtc = DateTime.UtcNow;
            var service = HttpClientHelpers.CreateClintConnectorService();
            var result = await service.DownloadDataDailyAsync(currentDateUtc, Guid.NewGuid().ToString());
            var dateFromResult = result.TimedRates.FirstOrDefault()?.ActualDateTime;

            Assert.NotNull(result);
            Assert.NotNull(dateFromResult);
            Assert.AreEqual(currentDateUtc, dateFromResult);
        }

        [Test]
        public async Task DownloadDailyRates_WhenFutureDateCome_ExceptionExpected()
        {
            var currentDateUtc = DateTime.UtcNow.AddDays(5);
            var service = HttpClientHelpers.CreateClintConnectorService();
            async  Task ActAction() => await service.DownloadDataDailyAsync(currentDateUtc, Guid.NewGuid().ToString());

            Assert.ThrowsAsync<ArgumentNullException>(ActAction);
        }

    }
}