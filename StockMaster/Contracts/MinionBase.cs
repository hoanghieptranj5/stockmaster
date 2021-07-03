using System;
using OpenQA.Selenium;
using StockMaster.Drivers;
using StockMaster.Services.Selenium;

namespace StockMaster.Contracts
{
    /// <summary>
    /// Just like a Decorator that wraps IWebDriver
    /// </summary>
    public abstract class MinionBase
    {
        protected SeleniumService SeleniumService { get; set; }

        private void SetUp()
        {
            SeleniumService = new SeleniumService(WebDriverSingleton.WebDriver);
        }

        private void TearDown()
        {
            SeleniumService.CloseDriver();
        }

        protected abstract void MainMethod();

        public void Execute()
        {
            SetUp();

            MainMethod();

            TearDown();
        }
    }
}
