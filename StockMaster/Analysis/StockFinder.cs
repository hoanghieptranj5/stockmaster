using System;
using System.Collections.Generic;
using System.Linq;
using StockMaster.Models.CoPhieu69;
using StockMaster.Models.VnDirect;
using StockMaster.Services.Calculators;
using StockMaster.Services.Files;
using StockMaster.Services.FolderServices;
using StockMaster.Services.Logger;

namespace StockMaster.Analysis
{
    public class StockFinder
    {
        private readonly ILoggerService _logger;

        public StockFinder(ILoggerService logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Works for HOSE only
        /// </summary>
        public void CompareCurrentPriceWithRecommendedPrice()
        {
            var fileService = new FileService.Builder().UseObjectStrategy().Build();
            var companies = fileService.Read<Company>(Environment.CurrentDirectory + "/" + FolderStructure.RESOURCES + "/vnindex_companies.csv");

            foreach (var company in companies)
            {
                _logger.Log("Collecting data for stock " + company.TickerName);

                var recommendations = fileService
                    .Read<VnDirectRecommendationOfCompany>(
                    Environment.CurrentDirectory
                    + "/" + FolderStructure.RECOMMENDS
                    + "/" + company.TickerName + ".csv");

                foreach (var recommend in recommendations)
                {
                    var percentageDiff = CalculatorService.ShowPercentageDifference(company.Price, recommend.Price);
                    var display = string.Format("{0}\t{1}\t{2}\t{3}", company.Price, recommend.Price, percentageDiff, recommend.CreatedDate);
                    _logger.Log(display);
                }
                _logger.Log("\n");
            }
        }

        /// <summary>
        /// HOSE only
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetTickerId()
        {
            var fileService = new FileService.Builder().UseObjectStrategy().Build();
            var records = fileService.Read<Company>(Environment.CurrentDirectory + "/" + FolderStructure.RESOURCES + "/vnindex_companies.csv");
            var stockIds = from record in records
                           select record.TickerName;

            return stockIds.ToList();
        }
    }
}
