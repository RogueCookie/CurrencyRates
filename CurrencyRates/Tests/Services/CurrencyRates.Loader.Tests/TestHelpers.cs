using System;
using System.Threading;
using System.Threading.Tasks;
using CurrencyRates.Loader.DAL;
using CurrencyRates.Loader.DAL.Repositories;
using CurrencyRates.Loader.MediatR.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CurrencyRates.Loader.Tests
{
    public class TestHelpers
    {
        public static LoaderContext CreateLoaderContextInMemory()
        {
            var context = new LoaderContext(new DbContextOptionsBuilder<LoaderContext>()
                .UseSqlite("Filename=:memory:")
                .UseSnakeCaseNamingConvention()
                .EnableSensitiveDataLogging()
                .Options);

            context.Database.EnsureDeleted();
            context.Database.OpenConnection();
            context.Database.EnsureCreated();
            return context;
        }

        public static LoaderContext CreateContextSQLite()
        {
            return new LoaderContext(new DbContextOptionsBuilder<LoaderContext>()
                .UseSqlite("Filename=pay.sqlite")
                .UseSnakeCaseNamingConvention()
                .Options);
        }

        //public static void MediatorAddNewRecord(Mock<IMediator> mockMediator, ICurrencyDailyRepository currencyDailyRepository, IProviderRepository providerRepository)
        //{
        //    var addNewRecordHandler = new StoreRatesCommandHandler(currencyDailyRepository, providerRepository, mockMediator.Object);

        //    Task<Unit> Predicate(AddNewRecordCommand addNewCommand, CancellationToken funcToken) =>
        //        addNewRecordHandler.Handle(addNewCommand, funcToken);

        //    // переопределили инициализацию медиатора
        //    mockMediator.Setup(x => x.Send(It.IsAny<AddNewRecordCommand>(), new CancellationToken()))
        //        .Returns((Func<AddNewRecordCommand, CancellationToken, Task<Unit>>)Predicate);
        //}
    }
}