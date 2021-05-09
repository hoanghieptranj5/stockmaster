using System.Collections.Generic;

namespace StockMaster.Services.Files
{
    public interface IWriteFileStrategy
    {
        public void Write<T>(string fileName, IEnumerable<T> entities);
        public IEnumerable<T> Read<T>(string filePath);
    }
}