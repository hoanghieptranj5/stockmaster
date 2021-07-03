using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace StockMaster.Services.Selenium
{
    public class SeleniumService
    {
        IWebDriver _webDriver;

        public SeleniumService(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public void GoTo(string url)
        {
            _webDriver.Url = url;
        }

        public void CloseDriver()
        {
            _webDriver.Close();
        }

        public void WaitFor(int seconds)
        {
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(seconds);
        }

        public IWebElement FindElementByXpath(string xpath)
        {
            return _webDriver.FindElement(By.XPath(xpath));
        }

        public ReadOnlyCollection<IWebElement> FindElementsByXpath(string xpath)
        {
            return _webDriver.FindElements(By.XPath(xpath));
        }

        public IWebElement FindInnerElementByXpath(IWebElement root, string xpath)
        {
            return root.FindElement(By.XPath(xpath));
        }

        public ReadOnlyCollection<IWebElement> FindInnerElementsByXpath(IWebElement root, string xpath)
        {
            return root.FindElements(By.XPath(xpath));
        }
    }
}
