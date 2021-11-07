using CurrencyRates.Core.Enums;
using CurrencyRates.Core.Models;
using CurrencyRates.Loader.MediatR.Commands;
using CurrencyRates.Loader.MediatR.Queries;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CurrencyRates.Loader.Tests.MediatR.Query
{
    public class ValidateResponseQueryHandlerTest
    {
        private ValidateResponseQueryHandler _validateResponseQueryHandler;

        [SetUp]
        public void Setup()
        {
            var mockLogger = new Mock<ILogger<ValidateResponseModel>>();
            _validateResponseQueryHandler = new ValidateResponseQueryHandler(mockLogger.Object);
        }

        [Test]
        public async Task Handle_WhenNotAllowedVersionCome_ExceptionExpected()
        {
            // Arrange
            var currentTime = DateTime.Now;
            var providerName = "Czech Bank";
            var version = "5.5";

            var initStoreRatesModel = InitStoreRates(currentTime, providerName, version);

            var request = new ValidateResponseModel()
            {
                CorrelationId = "id",
                Message = JsonConvert.SerializeObject(initStoreRatesModel)
            };

            // Act
            async Task ActAction() => await _validateResponseQueryHandler.Handle(request, new CancellationToken());

            // Asset
            Assert.ThrowsAsync<ArgumentNullException>(ActAction);
        }

        [Test]
        public async Task Handle_WhenDataIsValid_TimedCurrencyRatesModelExpected()
        {
            // Arrange
            var currentTime = DateTime.Now;
            var providerName = "Czech Bank";
            var version = "1.0";

            var initStoreRatesModel = InitStoreRates(currentTime, providerName, version);

            var request = new ValidateResponseModel()
            {
                CorrelationId = "id",
                Message = JsonConvert.SerializeObject(initStoreRatesModel)
            };

            // Act
            var result = await _validateResponseQueryHandler.Handle(request, new CancellationToken());

            // Asset
            Assert.NotNull(result);
            Assert.AreEqual(version, result.Version);
            Assert.AreEqual(providerName, result.SourceName);
            Assert.AreEqual(initStoreRatesModel.TimedRates.Count, result.TimedRates.Count);
        }

        private StoreRatesCommand InitStoreRates(DateTime currentTime, string providerName, string version)
        {
            var request = new StoreRatesCommand()
            {
                SourceName = providerName,
                Version = version,
                TimedRates = new List<LoaderCurrencyRatesModel>()
                {
                    new LoaderCurrencyRatesModel()
                    {
                        Rates = new List<DailyRates>()
                        {
                            new DailyRates()
                            {
                                CurrencyType = TypeOfCurrency.USD,
                                Rate = 71.936M,
                                Amount = 1
                            }
                        },
                        ActualDateTime = currentTime,
                        MasterCurrency = TypeOfCurrency.CZH
                    }
                }
            };

            return request;
        }
    }
}