using System;
using System.Collections.Generic;
using System.Linq;
using StockMaster.Models.CoPhieu69;
using StockMaster.Services.Files;

namespace StockMaster.Analysis
{
    public class StockFinder
    {
        /// <summary>
        /// Works for HOSE only
        /// </summary>
        public static void CompareCurrentPriceWithRecommendedPrice()
        {
            var fileService = new FileService.Builder().UseObjectStrategy().Build();
            var records = fileService.Read<Company>(Environment.CurrentDirectory + "/1_companies.csv");

            foreach (var record in records)
            {
                Console.WriteLine(record.Id + " : " + record.Name);
            }
        }

        /// <summary>
        /// HOSE only
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetStockIds()
        {
            var fileService = new FileService.Builder().UseObjectStrategy().Build();
            var records = fileService.Read<Company>(Environment.CurrentDirectory + "/1_companies.csv");
            var stockIds = from record in records
                           select record.Id;

            return stockIds.ToList();
        }
    }
}
