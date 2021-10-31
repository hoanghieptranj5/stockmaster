using System.Collections.Generic;
using StockMaster.Analysis;
using StockMaster.Contracts;
using StockMaster.Minions;
using StockMaster.Services.Files;
using StockMaster.Services.Logger;

namespace StockMaster.Services
{
    public class AppLogic : IAppLogic
    {
        private readonly ILoggerService _logger;
        private readonly FileService _fileService;

        public AppLogic(ILoggerService logger, FileService fileService)
        {
            _logger = logger;
            _fileService = fileService;
        }

        public IEnumerable<string> CollectStockData()
        {
            var logic = new StockFinder(_logger, _fileService);
            return logic.GetStockIds();
        }

        public void CollectRecommendations(IEnumerable<string> stockIds)
        {
            var minion = new GetRecommendMinion(_fileService, stockIds);
            minion.Execute();
        }

        public void ComparePriceAndRecommendations()
        {
            var logic = new StockFinder(_logger, _fileService);
            logic.CompareCurrentPriceWithRecommendedPrice();
        }
    }
}