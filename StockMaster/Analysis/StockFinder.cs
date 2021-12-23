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
        private readonly FileService _fileService;

        public StockFinder(ILoggerService logger, FileService fileService)
        {
            _logger = logger;
            _fileService = fileService;
        }

        /// <summary>
        /// Works for HOSE only
        /// </summary>
        public void CompareCurrentPriceWithRecommendedPrice()
        {
            var companies = _fileService.Read<Company>(Environment.CurrentDirectory + "/" + FolderStructure.RESOURCES + "/1_companies.csv");

            foreach (var company in companies)
            {
                _logger.Log("Collecting data for stock " + company.Id);

                var recommendations = _fileService
                    .Read<VnDirectRecommendationOfCompany>(
                    Environment.CurrentDirectory
                    + "/" + FolderStructure.RECOMMENDS
                    + "/" + company.Id + ".csv");

                foreach (var recommend in recommendations)
                {
                    var percentageDiff = CalculatorService.ShowPercentageDifference(company.Price, recommend.Price);
                    var display = string.Format("{0}\t{1}\t{2}\t{3}\t{4}", company.Price, recommend.Price, percentageDiff, recommend.CreatedDate, recommend.CompanyName);
                    _logger.Log(display);
                }
                _logger.Log("\n");
            }
        }

        /// <summary>
        /// HOSE only
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetStockIds()
        {
            var records = _fileService.Read<Company>(Environment.CurrentDirectory + "/" + FolderStructure.RESOURCES + "/1_companies.csv");
            var stockIds = from record in records
                           select record.Id;

            return stockIds.ToList();
        }
    }
}
