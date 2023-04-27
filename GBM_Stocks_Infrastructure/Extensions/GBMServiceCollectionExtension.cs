using GBM_Stocks_Infrastructure.Implementations;
using GBM_Stocks_Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GBM_Stocks_Infrastructure.Extensions
{
    public static class GBMServiceCollectionExtension
    {
        public static IServiceCollection AddAdBuilderServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITransact, Transact>();
            return services;
        }
    }
}