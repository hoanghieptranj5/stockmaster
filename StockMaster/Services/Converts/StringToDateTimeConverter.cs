using System;
namespace StockMaster.Services.Converts
{
    public class StringToDateTimeConverter
    {
        public static DateTime Convert(string timeString, string format)
        {
            var result = DateTime.ParseExact(timeString, format,
                System.Globalization.CultureInfo.InvariantCulture);

            return result;
        }
    }
}
