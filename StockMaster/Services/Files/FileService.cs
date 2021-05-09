using System;
using System.Collections.Generic;

namespace StockMaster.Services.Files
{
    /// <summary>
    /// Combined Strategy Pattern and Builder Pattern
    /// </summary>
    public class FileService
    {
        private IWriteFileStrategy _writeFileStrategy;

        private FileService()
        {

        }

        public void Write<T>(string fileName, IEnumerable<T> entities)
        {
            _writeFileStrategy.Write(fileName, entities);
        }

        public IEnumerable<T> Read<T>(string filePath)
        {
            return _writeFileStrategy.Read<T>(filePath);
        }

        public class Builder
        {
            private FileService _fileService;

            public Builder()
            {
                _fileService = new FileService();
            }

            public Builder UseObjectStrategy()
            {
                _fileService._writeFileStrategy = new WriteByObjectStrategy();
                return this;
            }

            public FileService Build()
            {
                return _fileService;
            }
        }
    }
}
