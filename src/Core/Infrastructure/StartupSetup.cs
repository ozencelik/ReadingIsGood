using Core.Services.Books;
using Core.Services.Customers;
using Core.Services.Orders;
using Data.Context;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure
{
    public static class StartupSetup
    {
        #region Methods
        public static void AddDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString));
        }

        public static void RegisterDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(EfCoreRepository<>));

            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IStockService, StockService>();
        }

        public static void EnsureDatabaseCreated(this IServiceCollection services)
        {
            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                var context = serviceProvider.GetRequiredService<AppDbContext>();
                context.Database.EnsureCreated();
                Seeder.Initialize(serviceProvider);
            }
        }
        #endregion
    }
}
