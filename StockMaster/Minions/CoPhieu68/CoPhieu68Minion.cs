using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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

            var companies = GetAllCompanies();

            fileService.Write(
                Environment.CurrentDirectory + "/" + FolderStructure.RESOURCES + "/" + "vnindex" + "_companies.csv",
                companies);
        }

        private IEnumerable<Company> GetAllCompanies()
        {
            var stcId = "vnindex";
            var stcCompanies = new List<Company>();

            WebDriver.Url = CoPhieu68Xpath.GetCompanyListUrl(stcId);

            var rows = SeleniumService.FindElementsByXpath(CoPhieu68Xpath.CompanyTableRowXpath);
            int dynamicId = 0;
            foreach (var row in rows)
            {
                var innerTds = SeleniumService.FindInnerElementsByXpath(row, ".//td");
                
                stcCompanies.Add(new Company
                {
                    Id = ++dynamicId,
                    TickerName = SeleniumService.FindInnerElementByXpath(innerTds[0], ".//div[1]").Text,
                    FullName = SeleniumService.FindInnerElementByXpath(innerTds[0], ".//div[2]").Text,
                    Price = ConvertToDouble(innerTds[1].Text),
                    Changes = ConvertToDouble(innerTds[2].Text),
                    VolumeTwentyFourHour = ConvertToDouble(innerTds[3].Text),
                    VolumeFiftyTwoWeek = ConvertToDouble(innerTds[4].Text),
                    VolumeRegistered = ConvertToDouble(innerTds[5].Text),
                    MarketCap = ConvertToDouble(innerTds[6].Text),
                    Chart = string.Empty
                });
            }

            return stcCompanies;
        }
        
        private static double ConvertToDouble(string input)
        {
            // Use InvariantCulture to ensure "." is interpreted as a decimal point
            if (double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                return result;
            }
    
            throw new FormatException($"Invalid number format: {input}");
        }
    }
}