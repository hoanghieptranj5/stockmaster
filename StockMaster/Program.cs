using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StockMaster.Analysis;
using StockMaster.Minions;
using StockMaster.Minions.CoPhieu68;
using StockMaster.Services.FolderServices;
using StockMaster.Services.Logger;

namespace StockMaster
{
    class Program
    {
        /// <summary>
        /// DO NOT DELETE
        /// this function setup all available resources
        /// </summary>
        private static void StartUp()
        {
            new FolderBuilder();
        }

        static Task Main(string[] args)
        {
            StartUp();

            using IHost host = CreateHostBuilder(args).Build();
            using IServiceScope serviceScope = host.Services.CreateScope();
            var provider = serviceScope.ServiceProvider;

            #region MainMethodGoesHere

            // var minion = new CoPhieu68Minion();
            // minion.Execute();

            // var minion = new VnDirectMinion();
            // minion.Execute();
            
            var logger = provider.GetRequiredService<ILoggerService>();
            
            var logic = new StockFinder(logger);
            logic.CompareCurrentPriceWithRecommendedPrice();

            #endregion

            return host.RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                {
                    services.AddScoped<StockFinder>();
                    services.AddScoped<ILoggerService, FileLoggerService>();
                });
        }
    }
}
