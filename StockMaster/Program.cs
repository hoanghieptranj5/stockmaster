using System;
using System.Collections.Generic;
using StockMaster.Analysis;
using StockMaster.Minions;
using StockMaster.Minions.CoPhieu68;
using StockMaster.Services.Files;
using StockMaster.Services.FolderServices;

namespace StockMaster
{
    class Program
    {
        /// <summary>
        /// DO NOT DELETE
        /// this function setup all available resources
        /// </summary>
        private static void StartUp()
        {
            new FolderBuilder();
        }

        static void Main(string[] args)
        {
            StartUp();

            //var minion = new CoPhieu68Minion();
            //minion.Execute();

            StockFinder.CompareCurrentPriceWithRecommendedPrice();
        }
    }
}
