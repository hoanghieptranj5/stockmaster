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
        protected IWebDriver WebDriver { get; set; }
        protected SeleniumService SeleniumService { get; set; }

        private void SetUp()
        {
            WebDriver = WebDriverSingleton.WebDriver;
            SeleniumService = new SeleniumService(WebDriver);
        }

        private void TearDown()
        {
            WebDriver.Close();
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
