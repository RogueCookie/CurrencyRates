using CurrencyRates.Loader.DAL.Model;
using System.Threading.Tasks;

namespace CurrencyRates.Loader.DAL.Repositories
{
    /// <summary>
    /// Repository for manipulation with Provider table
    /// </summary>
    public interface IProviderRepository
    {
        /// <summary>
        /// Get provider with particular name
        /// </summary>
        /// <param name="aliasName">Provider name</param>
        /// <returns>Provider with particular name</returns>
        Task<Provider> GetProviderByNameAsync(string aliasName);
    }
}