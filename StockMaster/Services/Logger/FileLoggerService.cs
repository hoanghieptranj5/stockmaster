using System;
using System.IO;

namespace StockMaster.Services.Logger
{
    public class FileLoggerService : ILoggerService
    {
        private readonly string _fileName = "StockMasterLogs.txt";

        public FileLoggerService()
        {

        }

        public void Log(string lines)
        {
            // Append text to an existing file named "WriteLines.txt".
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(Environment.CurrentDirectory, _fileName), true))
            {
                outputFile.WriteLine(lines);
            }
        }
    }
}
