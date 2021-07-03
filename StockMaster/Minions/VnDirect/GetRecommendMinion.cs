using System;
using System.Collections.Generic;
using StockMaster.Contracts;
using StockMaster.Minions.Xpaths.VnDirect;
using StockMaster.Models.VnDirect;
using StockMaster.Services.Converts;
using StockMaster.Services.Files;
using StockMaster.Services.FolderServices;

namespace StockMaster.Minions
{
    /// <summary>
    /// Concrete Decorator
    /// </summary>
    public class GetRecommendMinion : MinionBase
    {
        private readonly FileService _fileService;
        private IEnumerable<string> _stockIds;
        
        public GetRecommendMinion(FileService fileService, IEnumerable<string> stockIds)
        {
            _fileService = fileService;
            _stockIds = stockIds;
        }

        protected override void MainMethod()
        {
            foreach (var stockId in _stockIds)
            {
                var items = GetCompanyRecommendationPrice(stockId);
                _fileService.Write(Environment.CurrentDirectory 
                                   + "/" + FolderStructure.RECOMMENDS + "/" + stockId + ".csv", items);
            }

        }

        private IEnumerable<VnDirectRecommendationOfCompany> GetCompanyRecommendationPrice(string stockId)
        {
            var result = new List<VnDirectRecommendationOfCompany>();
            SeleniumService.GoTo(VnDirectXpath.GetRecommendationPriceForStock(stockId));

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
