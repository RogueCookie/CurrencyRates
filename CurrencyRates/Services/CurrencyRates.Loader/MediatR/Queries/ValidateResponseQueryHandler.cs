using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CurrencyRates.Core.Models;
using MediatR;

namespace CurrencyRates.Loader.MediatR.Queries
{
    public class ValidateResponseModel : IRequest<TimedCurrencyRatesModel>
    {
        /// <summary>
        /// то что пришлро
        /// </summary>
        public string Message { get; set; }
    }

    public class ValidateResponseQueryHandler : IRequestHandler<ValidateResponseModel, TimedCurrencyRatesModel>
    {
        public Task<TimedCurrencyRatesModel> Handle(ValidateResponseModel request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}