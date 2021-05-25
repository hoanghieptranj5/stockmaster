using System;
using System.Collections;
using System.Collections.Generic;
using OpenQA.Selenium;
using StockMaster.Contracts;
using StockMaster.Minions.Xpaths.CoPhieu68;
using StockMaster.Models.CoPhieu69;
using StockMaster.Services.Converts;
using StockMaster.Services.Files;
using StockMaster.Services.FolderServices;

namespace StockMaster.Minions.CoPhieu68
{
    public class CoPhieu68Minion : MinionBase
    {
        protected override void MainMethod()
        {
            var fileService = new FileService.Builder()
                .UseObjectStrategy()
                .Build();

            var stcId = 1;
            var companies = GetAllCompanies();
            foreach (var innerCompanies in companies)
            {
                fileService.Write(Environment.CurrentDirectory + "/" + FolderStructure.RESOURCES + "/" + stcId + "_companies.csv", innerCompanies);
                stcId++;
            }
        }

        private IEnumerable<IEnumerable<Company>> GetAllCompanies()
        {
            var result = new List<List<Company>>();

            for (var stcId = 1; stcId <= 1; stcId++)
            {
                var stcCompanies = new List<Company>();
                for (var page = 1; page <= 16; page++)
                {
                    WebDriver.Url = CoPhieu68Xpath.GetCompanyListUrl(stcId, page);

                    var rows = SeleniumService.FindElementsByXpath(CoPhieu68Xpath.CompanyTableRowXpath);
                    foreach (var row in rows)
                    {
                        var innerTds = SeleniumService.FindInnerElementsByXpath(row, ".//td");
                        stcCompanies.Add(new Company
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
                            Price = StringToNumberConverter.ConvertToDouble(RemoveBraces(innerTds[11].Text)),
                            MarketCap = StringToNumberConverter.ConvertToDouble(innerTds[12].Text),
                            Chart = string.Empty
                        });
                    }
                }
                result.Add(stcCompanies);
            }

            return result;
        }

        private string RemoveBraces(string value)
        {
            var input = value.Substring(0, value.IndexOf("\n"));
            return input;
        }
    }
}
