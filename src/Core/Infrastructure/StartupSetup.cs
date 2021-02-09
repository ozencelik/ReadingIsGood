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
            var consumers = GetConsumersToInject().ToList();
            services.InjectEventConsumers(consumers);

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

        #region Helper Methods

        public static IEnumerable<Type> GetConsumersToInject()
        {
            var type = typeof(IConsumer<>);
            var types = new List<Type>();
            foreach (Type mytype in Assembly.GetExecutingAssembly().GetTypes()
                .Where(mytype => mytype.GetInterfaces().Contains(type)))
            {
                types.Add(mytype);
            }
            /*
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => type.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);
            */
            return types;
        }

        private static void InjectEventConsumers(this IServiceCollection services, IEnumerable<Type> consumers)
        {
            foreach (var consumer in consumers)
            {
                services.AddSingleton(consumer, typeof(IConsumer<>));
            }
        }
        #endregion
    }
}
