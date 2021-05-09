using System;
namespace StockMaster.Models.CoPhieu69
{
    public class Company
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime FirstDate { get; set; }
        public double FirstTimeVolume { get; set; }
        public double FirstPrice { get; set; }
        public double CurrentVolume { get; set; }
        public double TreasuryShares { get; set; }
        public double ListedVolume { get; set; }
        public double ForeignOwn { get; set; }
        public double ForeignAllowedToBuy { get; set; }
        public double Price { get; set; }
        public double MarketCap { get; set; }
        public string Chart { get; set; }
    }
}
