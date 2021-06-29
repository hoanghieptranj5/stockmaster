using System;
using static StockMaster.Services.FolderServices.FolderStructure;
namespace StockMaster.Services.FolderServices
{
    public class FolderBuilder
    {
        public FolderBuilder()
        {
            var folders = new [] { DATA, RESOURCES, RECOMMENDS };
            MakeFolders(folders);
        }

        #region Private Methods

        private static void MakeFolders(string[] folders)
        {
            foreach (var folder in folders)
            {
                System.IO.Directory.CreateDirectory(Environment.CurrentDirectory + "/" + folder);
            }
        }

        #endregion
        
    }
}
