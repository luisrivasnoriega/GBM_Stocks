using GBM_Stocks_Orders_Core.Interfaces;
using GBM_Stocks_Orders_Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GBM_Stocks_Orders_Core.Extensions
{
    public static class StocksOrdersCoreCollection
    {
        public static IServiceCollection StocksOrdersCoreServiceCollection(this IServiceCollection services)
        {
            services.AddScoped<IOrdersService, OrdersService>();
            return services;
        }
    }
}