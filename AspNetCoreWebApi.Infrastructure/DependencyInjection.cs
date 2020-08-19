using Microsoft.Extensions.DependencyInjection;
using AspNetCoreWebApi.Core.Logging;
using AspNetCoreWebApi.Core.Repository;
using AspNetCoreWebApi.Core.Repository.Base;
using AspNetCoreWebApi.Infrastructure.Logging;
using AspNetCoreWebApi.Infrastructure.Repository;
using AspNetCoreWebApi.Infrastructure.Repository.Base;

namespace AspNetCoreWebApi.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            // repositories
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            return services;
        }
    }
}
