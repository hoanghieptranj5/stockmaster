using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StockMaster.Analysis;
using StockMaster.Contracts;
using StockMaster.Minions;
using StockMaster.Minions.CoPhieu68;
using StockMaster.Services;
using StockMaster.Services.Files;
using StockMaster.Services.FolderServices;
using StockMaster.Services.Logger;

namespace StockMaster
{
    class Program
    {
        #region Private Methods

        /// <summary>
        /// DO NOT DELETE
        /// this function setup all available resources
        /// </summary>
        private static void StartUp()
        {
            new FolderBuilder();
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Dependencies Injection Goes Here
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                {
                    services.AddScoped<StockFinder>();
                    services.AddScoped<FileService>();
                    services.AddScoped<ILoggerService, FileLoggerService>();
                    services.AddScoped<IAppLogic, AppLogic>();
                });
        }

        #endregion
        
        static Task Main(string[] args)
        {
            StartUp();

            using IHost host = CreateHostBuilder(args).Build();
            using IServiceScope serviceScope = host.Services.CreateScope();
            var provider = serviceScope.ServiceProvider;

            #region Populate Services
            
            var appLogic = provider.GetRequiredService<IAppLogic>();

            #endregion

            #region MainMethodGoesHere

            // var ids = appLogic.CollectStockData();
            
            // appLogic.CollectRecommendations(ids);
            
            appLogic.ComparePriceAndRecommendations();
            
            #endregion

            return host.RunAsync();
        }
    }
}
