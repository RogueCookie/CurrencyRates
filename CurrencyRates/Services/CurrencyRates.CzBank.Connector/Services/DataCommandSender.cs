using System.Threading.Tasks;
using CurrencyRates.Core.Models;
using CurrencyRates.CzBank.Connector.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CurrencyRates.CzBank.Connector.Services
{
    public class DataCommandSender : IDataCommandSender
    {
        public DataCommandSender(IOptions<RabbitSettings> options,
            IOptions<AddNewJobModel> registerSettings,
            ILogger<RabbitCommandHandlerService> logger)
        {
            
        }

        ///<inheritdoc />
        public Task SendDataToLoader(TimedCurrencyRatesModel timedCurrencyRatesModel)
        {
            throw new System.NotImplementedException();
        }
    }
}