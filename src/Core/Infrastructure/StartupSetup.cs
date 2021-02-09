using Core.Events.Consumers;
using Core.Services.Customers;
using Data.Context;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
