using System;
using System.Collections;
using System.Collections.Generic;
using OpenQA.Selenium;
using StockMaster.Analysis;
using StockMaster.Contracts;
using StockMaster.Minions.Xpaths.VnDirect;
using StockMaster.Models.VnDirect;
using StockMaster.Services.Converts;
using StockMaster.Services.Files;

namespace StockMaster.Minions
{
    /// <summary>
    /// Concrete Decorator
    /// </summary>
    public class VnDirectMinion : MinionBase
    {
        public VnDirectMinion()
        {
        }

        protected override void MainMethod()
        {
            foreach (var stockId in StockFinder.GetStockIds())
            {
                var items = GetCompanyRecommendationPrice(stockId);

                var fileService = new FileService
                                            .Builder()
                                            .UseObjectStrategy()
                                            .Build();
                fileService.Write(Environment.CurrentDirectory + "/" + stockId + "_recommends.csv", items);
            }

        }

        private IEnumerable<VnDirectRecommendationOfCompany> GetCompanyRecommendationPrice(string stockId)
        {
            var result = new List<VnDirectRecommendationOfCompany>();
            WebDriver.Url = VnDirectXpath.GetRecommendationPriceForStock(stockId);

            SeleniumService.WaitFor(1);

            var lines = SeleniumService.FindElementsByXpath(VnDirectXpath.CompanyRecommendPriceLineXpath);
            foreach (var line in lines)
            {
                var innerTds = SeleniumService.FindInnerElementsByXpath(
                        line,
                        VnDirectXpath.GetInnerElementXpath(VnDirectXpath.InnerTd)
                    );

                var recommendItem = new VnDirectRecommendationOfCompany
                {
                    CreatedDate = StringToDateTimeConverter.Convert(innerTds[0].Text, "dd/MM/yyyy"),
                    CompanyName = innerTds[1].Text,
                    Recommend = innerTds[2].Text,
                    Price = StringToNumberConverter.ConvertToDouble(innerTds[3].Text)
                };

                result.Add(recommendItem);
            }

            return result;
        }
    }
}
