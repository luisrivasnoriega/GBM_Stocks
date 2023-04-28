using GBM_Stocks_Core_Infrastructure.Interfaces;
using GBM_Stocks_Core_Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace GBM_Stocks_Orders_Infrastructure.Extensions
{
    public static class StocksOrdersRepositoryCollection
    {
        public static IServiceCollection StocksOrdersRepositoryServiceCollection(this IServiceCollection services)
        {
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            return services;
        }
    }
}
