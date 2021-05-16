using System;
using static StockMaster.Services.FolderServices.FolderStructure;
namespace StockMaster.Services.FolderServices
{
    public class FolderBuilder
    {
        public FolderBuilder()
        {
            var folders = new string[] { DATA, RESOURCES, RECOMMENDS };
            MakeFolders(folders);
        }

        private static void MakeFolders(string[] folders)
        {
            foreach (var folder in folders)
            {
                System.IO.Directory.CreateDirectory(Environment.CurrentDirectory + "/" + folder);
            }
        }
    }
}
