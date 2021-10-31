using System.Collections.Generic;
using StockMaster.Analysis;
using StockMaster.Contracts;
using StockMaster.Minions;
using StockMaster.Minions.CoPhieu68;
using StockMaster.Services.Files;
using StockMaster.Services.Logger;

namespace StockMaster.Services
{
    public class AppLogic : IAppLogic
    {
        private readonly ILoggerService _logger;
        private readonly FileService _fileService;
        private readonly StockFinder _stockFinder;
        private readonly GetRecommendMinion _getRecommendMinion;
        private readonly StockInfoMinion _stockInfoMinion;

        public AppLogic(ILoggerService logger, 
            FileService fileService, 
            StockFinder stockFinder, 
            GetRecommendMinion getRecommendMinion, 
            StockInfoMinion stockInfoMinion)
        {
            _logger = logger;
            _fileService = fileService;
            _stockFinder = stockFinder;
            _getRecommendMinion = getRecommendMinion;
            _stockInfoMinion = stockInfoMinion;
        }

        public void CollectStockData()
        {
            _stockInfoMinion.Execute();
        }

        public void CollectRecommendations()
        {
            _getRecommendMinion.StockIds = _stockFinder.GetStockIds();
            _getRecommendMinion.Execute();
        }

        public void ComparePriceAndRecommendations()
        {
            _stockFinder.CompareCurrentPriceWithRecommendedPrice();
        }
    }
}