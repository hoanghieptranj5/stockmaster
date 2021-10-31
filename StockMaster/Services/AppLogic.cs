using StockMaster.Analysis;
using StockMaster.Contracts;
using StockMaster.Minions;
using StockMaster.Minions.CoPhieu68;

namespace StockMaster.Services
{
    public class AppLogic : IAppLogic
    {
        private readonly StockFinder _stockFinder;
        private readonly GetRecommendMinion _getRecommendMinion;
        private readonly StockInfoMinion _stockInfoMinion;

        public AppLogic( 
            StockFinder stockFinder, 
            GetRecommendMinion getRecommendMinion, 
            StockInfoMinion stockInfoMinion)
        {
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