using GBM_Stocks_Accounts_Core.Interfaces;
using GBM_Stocks_Accounts_Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GBM_Stocks_Accounts_Core.Extensions
{
    public static class StocksAccountsCoreCollection
    {
        public static IServiceCollection StocksAccountsCoreServiceCollection(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            return services;
        }
    }
}
