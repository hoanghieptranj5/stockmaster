using System;
namespace StockMaster.Minions.Xpaths.CoPhieu68
{
    public class CoPhieu68Xpath
    {
        public static readonly string CompanyTableRowXpath = "//tr[@onmouseover='hoverTR(this)']";

        public static string GetCompanyListUrl(int stcId, int pageNumber)
        {
            return string.Format("https://www.cophieu68.vn/companylist.php?keyword=&category=&stcid={0}&search=T%C3%ACm+Ki%E1%BA%BFm&currentPage={1}", stcId, pageNumber);
        }
    }
}
