using System;
namespace StockMaster.Services.Logger
{
    public class ConsoleLoggerService : ILoggerService
    {
        public void Log(string line)
        {
            Console.WriteLine(line);
        }
    }
}
