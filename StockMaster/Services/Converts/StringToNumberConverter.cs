using System;
namespace StockMaster.Services.Converts
{
    public class StringToNumberConverter
    {
        public static double ConvertToDouble(string numberString)
        {
            double outVar;
            double.TryParse(numberString, out outVar);
            if (double.IsNaN(outVar) || double.IsInfinity(outVar))
            {
                return 0;
            }

            return outVar;
        }
    }
}
