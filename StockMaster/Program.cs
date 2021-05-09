using System;
using System.Collections.Generic;
using StockMaster.Analysis;
using StockMaster.Minions;
using StockMaster.Minions.CoPhieu68;
using StockMaster.Services.Files;

namespace StockMaster
{
    class Program
    {
        static void Main(string[] args)
        {


            var minion = new VnDirectMinion();
            minion.Execute();

            //StockFinder.CompareCurrentPriceWithRecommendedPrice();
        }
    }
}
