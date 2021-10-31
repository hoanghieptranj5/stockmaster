using System.Collections.Generic;

namespace StockMaster.Contracts
{
    public interface IAppLogic
    {
        void CollectStockData();
        void CollectRecommendations();
        void ComparePriceAndRecommendations();
    }
}