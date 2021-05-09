using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;

namespace StockMaster.Services.Files
{
    public class WriteByObjectStrategy : IWriteFileStrategy
    {
        public void Write<T>(string filePath, IEnumerable<T> entities)
        {
            using (StreamWriter sw = new(filePath, false, new UTF8Encoding(true)))
            using (CsvWriter cw = new(sw, CultureInfo.InvariantCulture))
            {
                cw.WriteHeader<T>();
                cw.NextRecord();
                foreach (var entity in entities)
                {
                    cw.WriteRecord(entity);
                    cw.NextRecord();
                }
            }
        }

        public IEnumerable<T> Read<T>(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<T>().ToList();
                return records;
            }
        }
    }
}
