using System;
namespace StockMaster.Minions.Xpaths.VnDirect
{
    public class VnDirectXpath
    {
        public static readonly string CompanyRecommendPriceLineXpath = "//table//tr[not(@class='text-small')]";

        public static readonly string InnerTd = "td";

        public static string GetInnerElementXpath(string innerXpath)
        {
            return string.Format(".//{0}", innerXpath);
        }

        public static string GetRecommendationPriceForStock(string stockId)
        {
            return string.Format("https://dstock.vndirect.com.vn/tong-quan/{0}/quan-diem-cac-cong-ty-ck-popup", stockId.ToUpper());
        }
    }
}
