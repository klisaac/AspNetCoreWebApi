using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using NLog.Web;
using AspNetCoreWebApi.Core.Configuration;
using AspNetCoreWebApi.Core.Logging;
using AspNetCoreWebApi.Infrastructure.Data;

namespace AspNetCoreWeb.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            SeedData(host);
            host.Run();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.Configure<KestrelServerOptions>(
                        context.Configuration.GetSection(Constants.KestrelKey));
                })
                .ConfigureAppConfiguration((context, config) =>
                {
                    var env = context.HostingEnvironment;
                    //config.AddEnvironmentVariables();
                    //config.AddCommandLine(args);
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                    var root = config.Build();
                    config.AddAzureKeyVault($"https://{root["KeyVault:Vault"]}.vault.azure.net/", root["KeyVault:ClientId"], root["KeyVault:ClientSecret"]);
                })
                .ConfigureLogging((context, logging) =>
                {
                    // clear all previously registered providers
                    logging.ClearProviders();
                    logging.AddConfiguration(context.Configuration.GetSection(Constants.LoggingKey));
                    if (context.HostingEnvironment.IsDevelopment())
                    {
                        logging.AddConsole();
                        logging.AddDebug();
                    }
                    // Enable NLog as one of the Logging Provider
                    //logging.AddNLog();
                })
                .UseNLog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //webBuilder.ConfigureKestrel(serverOptions =>
                    //{
                    //    // Set properties and call methods on options
                    //    serverOptions.Limits
                    //})
                    webBuilder.UseStartup<Startup>();
                });

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        private static void SeedData(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<IAppLogger<AppDataSeed>>();
                try
                {
                    var AspNetCoreWebContext = services.GetRequiredService<AppDataContext>();
                    AppDataSeed.SeedAsync(AspNetCoreWebContext, true).Wait();
                }
                catch (Exception ex)
                {
                    logger.LogError("An error occurred seeding the DB.", ex);
                }
            }
        }
    }
}
