using Microsoft.Extensions.DependencyInjection;

namespace GBM_Stocks_Accounts_Core.Extensions
{
    public static class StocksAccountsCoreCollection
    {
        public static IServiceCollection StocksAccountsCore(this IServiceCollection services)
        {
            //services.AddScoped<IDealerRepository, DealerRepository>();
            //services.AddScoped<IDealerService, DealerService>();
            return services;
        }
    }
}
