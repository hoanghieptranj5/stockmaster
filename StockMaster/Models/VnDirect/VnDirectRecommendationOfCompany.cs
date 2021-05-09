using System;
namespace StockMaster.Models.VnDirect
{
    public class VnDirectRecommendationOfCompany
    {
        public DateTime CreatedDate { get; set; }

        public string CompanyName { get; set; }

        public string Recommend { get; set; }

        public double Price { get; set; }
    }
}
