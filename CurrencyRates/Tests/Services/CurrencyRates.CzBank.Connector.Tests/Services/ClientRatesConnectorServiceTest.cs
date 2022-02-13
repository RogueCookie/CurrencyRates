using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CurrencyRates.CzBank.Connector.Tests.Services
{
    public class ClientRatesConnectorServiceTest
    {
        [Test]
        public async Task DownloadDailyRates_ModelWithListOfRatesExpected()
        {
            var currentDateUtc = DateTime.UtcNow;
            var token = new CancellationToken();
            var service = HttpClientHelpers.CreateClintConnectorService();
            var result = await service.DownloadDataDailyAsync(currentDateUtc, Guid.NewGuid().ToString(), token);
            var dateFromResult = result.TimedRates.FirstOrDefault()?.ActualDateTime;

            Assert.NotNull(result);
            Assert.NotNull(dateFromResult);
            Assert.AreEqual(currentDateUtc, dateFromResult);
        }

        [Test]
        public async Task DownloadDailyRates_WhenFutureDateCome_ExceptionExpected()
        {
            var currentDateUtc = DateTime.UtcNow.AddDays(5);
            var token = new CancellationToken();
            var service = HttpClientHelpers.CreateClintConnectorService();
            async  Task ActAction() => await service.DownloadDataDailyAsync(currentDateUtc, Guid.NewGuid().ToString(), token);

            Assert.ThrowsAsync<ArgumentNullException>(ActAction);
        }

        [Test]
        public async Task DownloadYearlyRates_WhenFutureYearCome_ExceptionExpected()
        {
            var currentDateUtc = DateTime.UtcNow.AddYears(1);
            var token = new CancellationToken();
            var service = HttpClientHelpers.CreateClintConnectorService();
            async Task ActAction() => await service.DownloadDataDailyAsync(currentDateUtc, Guid.NewGuid().ToString(), token);

            Assert.ThrowsAsync<ArgumentNullException>(ActAction);
        }

        [Test]
        public async Task DownloadYearlyRates_WhenDateCome_DataFromWholeYearExpected()
        {
            var currentDateUtc = DateTime.UtcNow.AddYears(-1);
            var token = new CancellationToken();
            var service = HttpClientHelpers.CreateClintConnectorService();
            var result = await service.DownloadDataYearlyAsync(currentDateUtc, Guid.NewGuid().ToString(), token);

            var expectedRatesCountPerDay = 33;
            var expectedRatesPerYear = 251;

            Assert.NotNull(result);
            Assert.AreEqual(expectedRatesPerYear, result.TimedRates.Count);
            foreach (var rates in result.TimedRates)
            {
                Assert.AreEqual(expectedRatesCountPerDay, rates.Rates.Count);
            }
        }
    }
}