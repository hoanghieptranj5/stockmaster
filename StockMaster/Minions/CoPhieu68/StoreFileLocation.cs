using System;
using StockMaster.Services.FolderServices;

namespace StockMaster.Minions.CoPhieu68
{
    public static class StoreFileLocation
    {
        public static string StcIds(int stcId)
        {
            return $"{Environment.CurrentDirectory}/{FolderStructure.RESOURCES}/{stcId}_companies.csv";
        }
    }
}