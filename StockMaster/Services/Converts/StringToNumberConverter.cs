using System;
using System.Globalization;

namespace StockMaster.Services.Converts
{
    public class StringToNumberConverter
    {
        public static double ConvertToDouble(string input)
        {
            // Use InvariantCulture to ensure "." is interpreted as a decimal point
            if (double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                return result;
            }
    
            throw new FormatException($"Invalid number format: {input}");
        }
    }
}
