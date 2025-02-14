using System;
namespace StockMaster.Minions.Xpaths.CoPhieu68
{
    public class CoPhieu68Xpath
    {
        public static readonly string CompanyTableRowXpath = "//tr[@onmouseover='hoverTR(this)']";

        public static string GetCompanyListUrl(string stcId)
        {
            return string.Format("https://www.cophieu68.vn/market/markets.php?id=^vnindex", stcId);
        }
    }
}
