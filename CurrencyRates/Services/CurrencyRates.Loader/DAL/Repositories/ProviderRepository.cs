using CurrencyRates.Loader.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CurrencyRates.Loader.DAL.Repositories
{
    public class ProviderRepository : IProviderRepository
    {
        private readonly LoaderContext _loaderContext;

        public ProviderRepository(LoaderContext loaderContext)
        {
            _loaderContext = loaderContext;
        }

        /// <inheritdoc /> 
        public async Task<Provider> GetProviderByNameAsync(string aliasName)
        {
           return await _loaderContext.Providers.SingleOrDefaultAsync(x => x.ProviderName == aliasName);
        }
    }
}