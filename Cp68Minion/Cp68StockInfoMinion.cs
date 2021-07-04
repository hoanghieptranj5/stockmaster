using System;
using System.Collections.Generic;
using Cp68Minion.Models;
using Cp68Minion.Utils;
using StockMaster.Contracts;
using StockMaster.Minions.Xpaths.CoPhieu68;
using StockMaster.Models.CoPhieu69;
using StockMaster.Services.Converts;
using StockMaster.Services.Files;

namespace Cp68Minion
{
    public class Cp68StockInfoMinion : MinionBase
    {
        private readonly FileService _fileService;

        public Cp68StockInfoMinion(FileService fileService)
        {
            _fileService = fileService;
        }

        protected override void MainMethod()
        {
            var companyList = new CompanyListModel
            {
                Stc = Stc.StcIds[0],
                Companies = new List<Company>()
            };
            
            throw new NotImplementedException();
        }
        
        public List<CompanyListModel> GetAll()
        {
            var result = new List<CompanyListModel>();

            foreach (var stc in Stc.StcIds)
            {
                var tempoList = new CompanyListModel();
                
                var companiesByStc = new List<Company>();
                for (var page = 1; page <= 16; page++)
                {
                    SeleniumService.GoTo(CoPhieu68Xpath.GetCompanyListUrl(stc.Key, page));

                    var rows = SeleniumService.FindElementsByXpath(CoPhieu68Xpath.CompanyTableRowXpath);
                    foreach (var row in rows)
                    {
                        var innerTds = SeleniumService.FindInnerElementsByXpath(row, ".//td");
                        companiesByStc.Add(new Company
                        {
                            Id = innerTds[1].Text,
                            Name = innerTds[2].Text,
                            FirstDate = StringToDateTimeConverter.Convert(innerTds[3].Text, "dd/mm/yyyy"),
                            FirstTimeVolume = StringToNumberConverter.ConvertToDouble(innerTds[4].Text),
                            FirstPrice = StringToNumberConverter.ConvertToDouble(innerTds[5].Text),
                            CurrentVolume = StringToNumberConverter.ConvertToDouble(innerTds[6].Text),
                            TreasuryShares = StringToNumberConverter.ConvertToDouble(innerTds[7].Text),
                            ListedVolume = StringToNumberConverter.ConvertToDouble(innerTds[8].Text),
                            ForeignOwn = StringToNumberConverter.ConvertToDouble(innerTds[9].Text),
                            ForeignAllowedToBuy = StringToNumberConverter.ConvertToDouble(innerTds[10].Text),
                            Price = StringToNumberConverter.ConvertToDouble(StringUtils.RemoveBraces(innerTds[11].Text)),
                            MarketCap = StringToNumberConverter.ConvertToDouble(innerTds[12].Text),
                            Chart = string.Empty
                        });
                    }
                }

                tempoList.Stc = stc;
                tempoList.Companies = companiesByStc;
                
                result.Add(tempoList);
            }

            return result;
        }
    }
}