using System;
namespace StockMaster.Services.Calculators
{
    public class CalculatorService
    {
        public static string ShowPercentageDifference(double baseNumber, double compareNumber)
        {
            var percentage = (compareNumber - baseNumber) / baseNumber * 100;
            return string.Format("{0}%", percentage.ToString("0.00"));
        }
    }
}
