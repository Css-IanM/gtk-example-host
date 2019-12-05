using System;
using Jupiter.Budgetary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.IO;
using Gtk;

namespace Jupiter
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.Init();
            // Build the provider to handle injection.
            var host = CreateHostBuilder(args).Build();
            var app = host.Services.GetService<Startup>();
            app.Start();
            Application.Run();
            // Resolve App startup from IoC Container so any future depedencies can be provided by the container
            // should it request them, like app level permissions via services, configuration, or logging services.
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.SetBasePath($"{Directory.GetCurrentDirectory()}\\config");
                    config.AddJsonFile("appsettings.json");
                })
                .ConfigureServices((hostingContext, services) =>
                {
                    services.AddDbContext<BudgetaryContext>(options =>
                    {
                        options.UseSqlServer(hostingContext.Configuration.GetConnectionString("BudgetaryDatabase"));
                    });
                    services.AddLogging(options =>
                    {
                        options.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    });
                    services.AddSingleton<Application>(new Application("org.Jupiter.Jupiter", GLib.ApplicationFlags.None));
                    services.AddSingleton<Startup>();
                    services.AddTransient<LoginWindow>();
                })
                .ConfigureLogging((hostContext, logging) =>
                {
                    logging.AddConsole();
                })
                .UseConsoleLifetime();
    }
}