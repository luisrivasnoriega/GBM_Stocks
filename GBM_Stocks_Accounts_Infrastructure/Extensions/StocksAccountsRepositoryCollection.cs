using GBM_Stocks_Accounts_Infrastructure.Interfaces;
using GBM_Stocks_Accounts_Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace GBM_Stocks_Accounts_Infrastructure.Extensions
{
    public static class StocksAccountsRepositoryCollection
    {
        public static IServiceCollection StocksAccountsRepositoryServiceCollection(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            return services;
        }
    }
}
