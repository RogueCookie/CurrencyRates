using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CurrencyRates.Core.Enums;
using CurrencyRates.Core.Models;
using CurrencyRates.Loader.DAL;
using CurrencyRates.Loader.DAL.Repositories;
using CurrencyRates.Loader.MediatR.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace CurrencyRates.Loader.Tests.MediatR.Command
{
    public class StoreRatesCommandHandlerTest
    {
        private LoaderContext _context;
        private Mock<IMediator> _mockMediator;
        private StoreRatesCommandHandler _storeRatesHandler;
        private ICurrencyDailyRepository _currencyDailyRepository;
        private IProviderRepository _providerRepository;

        [SetUp]
        public void Setup()
        {
            _context = TestHelpers.CreateLoaderContextInMemory();

            var mockLogger = new Mock<ILogger<StoreRatesCommandHandler>>();
            var mockLoggerDailyRepo = new Mock<ILogger<CurrencyDailyRepository>>();
            _mockMediator = new Mock<IMediator>();
            _currencyDailyRepository = new CurrencyDailyRepository(_context, mockLoggerDailyRepo.Object);
            _providerRepository = new ProviderRepository(_context);
            _storeRatesHandler = new StoreRatesCommandHandler(_currencyDailyRepository, _providerRepository, mockLogger.Object);
        }

        [Test]
        public async Task Handle_WhenNewDataCome_DefaultExpected()
        {
            // Arrange
            var currentTime = DateTime.Now;
            var providerName = "Czech Bank";
            var initStoreRatesModel = InitStoreRates(currentTime, providerName);
            
            // Act
            await _storeRatesHandler.Handle(initStoreRatesModel, new CancellationToken());
            var result = await _context.CurrencyRatesDailies.CountAsync();

            // Asset
            Assert.Pass();
            Assert.AreEqual(1, result);
        }

        [Test]
        public async Task Handle_WhenUnexpectedProviderName_ExceptionExpected()
        {
            // Arrange
            var currentTime = DateTime.Now;
            var providerName = "test";
            var initStoreRatesModel = InitStoreRates(currentTime, providerName);

            // Act
            async Task ActAction() => await _storeRatesHandler.Handle(initStoreRatesModel, new CancellationToken());

            // Asset
            Assert.ThrowsAsync<ArgumentNullException>(ActAction);
        }

        [Test]
        public async Task Handle_WhenTheSameDataCome_DuplicatedDateNotStoredExpected()
        {
            // Arrange
            var currentTime = DateTime.Now;
            var providerName = "Czech Bank";
            var initStoreRatesModel = InitStoreRates(currentTime, providerName);

            // Act
            await _storeRatesHandler.Handle(initStoreRatesModel, new CancellationToken());
            var resultAfterFirstStoring = await _context.CurrencyRatesDailies.CountAsync();

            await _storeRatesHandler.Handle(initStoreRatesModel, new CancellationToken());
            var resultAfterSecondStoring = await _context.CurrencyRatesDailies.CountAsync();

            // Asset
            Assert.AreEqual(resultAfterFirstStoring, resultAfterSecondStoring);
        }


        private StoreRatesCommand InitStoreRates(DateTime currentTime, string providerName)
        {
            var request = new StoreRatesCommand()
            {
                SourceName = providerName,
                Version = "1.0",
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