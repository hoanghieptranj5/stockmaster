using System;
using System.Collections.Generic;
using System.Linq;
using StockMaster.Models.CoPhieu69;
using StockMaster.Models.VnDirect;
using StockMaster.Services.Calculators;
using StockMaster.Services.Files;
using StockMaster.Services.FolderServices;

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
            var companies = fileService.Read<Company>(Environment.CurrentDirectory + "/" + FolderStructure.RESOURCES + "/1_companies.csv");

            foreach (var company in companies)
            {
                Console.WriteLine("Collecting data for stock " + company.Id);

                var recommendations = fileService
                    .Read<VnDirectRecommendationOfCompany>(
                    Environment.CurrentDirectory
                    + "/" + FolderStructure.RECOMMENDS
                    + "/" + company.Id + ".csv");

                foreach (var recommend in recommendations)
                {
                    var percentageDiff = CalculatorService.ShowPercentageDifference(company.Price, recommend.Price);
                    var display = string.Format("{0}\t{1}\t{2}\t{3}", company.Price, recommend.Price, percentageDiff, recommend.CreatedDate);
                    Console.WriteLine(display);
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// HOSE only
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetStockIds()
        {
            var fileService = new FileService.Builder().UseObjectStrategy().Build();
            var records = fileService.Read<Company>(Environment.CurrentDirectory + "/" + FolderStructure.RESOURCES + "/1_companies.csv");
            var stockIds = from record in records
                           select record.Id;

            return stockIds.ToList();
        }
    }
}
