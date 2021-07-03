﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StockMaster.Analysis;
using StockMaster.Minions;
using StockMaster.Minions.CoPhieu68;
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
                });
        }

        static void ExemplifyScoping(IServiceProvider services, string scope)
        {
            using IServiceScope serviceScope = services.CreateScope();
            var provider = serviceScope.ServiceProvider;

            var logger = provider.GetRequiredService<ILoggerService>();
            logger.Log("Logger is ready for service.");
        }

        #endregion
        
        static Task Main(string[] args)
        {
            StartUp();

            using IHost host = CreateHostBuilder(args).Build();
            using IServiceScope serviceScope = host.Services.CreateScope();
            var provider = serviceScope.ServiceProvider;

            #region MainMethodGoesHere
            
            var logger = provider.GetRequiredService<ILoggerService>();
            var fileService = provider.GetRequiredService<FileService>();
            
            var logic = new StockFinder(logger, fileService);
            var stockIds = logic.GetStockIds();

            var minion = new StockInfoMinion(fileService);
            minion.Execute();
            
            // var minion = new GetRecommendMinion(fileService, stockIds);
            // minion.Execute();
            //
            // logic.CompareCurrentPriceWithRecommendedPrice();

            #endregion

            return host.RunAsync();
        }
    }
}
