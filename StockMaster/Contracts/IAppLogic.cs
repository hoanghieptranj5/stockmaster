using System.Collections.Generic;

namespace StockMaster.Contracts
{
    public interface IAppLogic
    {
        IEnumerable<string> CollectStockData();
        void CollectRecommendations(IEnumerable<string> stockIds);
        void ComparePriceAndRecommendations();
    }
}