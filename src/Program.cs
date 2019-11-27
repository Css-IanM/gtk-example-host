using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

namespace Jupiter
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            // Create and Configure Services
            IServiceCollection services = ConfigureServices();

            // Build the provider to handle injection.
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            // Resolve App startup from IoC Container so any future depedencies can be provided by the container
            // should it request them, like app level permissions via services, configuration, or logging services.
            serviceProvider.GetService<Startup>().Run();
        }

        private static IServiceCollection ConfigureServices()
        {
            ServiceCollection services = new ServiceCollection();

            // WIP Prototype using MS Idendtity system for roles.
            services.AddIdentityCore<IdentityUser>(opt =>
            {
                opt.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<DbContext>();

            // Register our App Startup with the container.
            services.AddTransient<Startup>();

            return services;
        }

    }
}
