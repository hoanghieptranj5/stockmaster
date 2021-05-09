using System;
namespace StockMaster.Services.FolderServices
{
    public class FolderBuilder
    {
        public FolderBuilder()
        {
            var folders = new string[] { "resources", "data" };
            MakeFolders(folders);
        }

        private void MakeFolders(string[] folders)
        {
            foreach (var folder in folders)
            {
                System.IO.Directory.CreateDirectory(Environment.CurrentDirectory + "/" + folder);
            }
        }
    }
}
