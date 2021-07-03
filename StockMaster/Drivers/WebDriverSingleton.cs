using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace StockMaster.Drivers
{
    public sealed class WebDriverSingleton
    {
        private static readonly object _lock = new object();

        private static IWebDriver _webDriver = null;

        public static IWebDriver WebDriver
        {
            get
            {
                lock (_lock)
                {
                    if (_webDriver == null)
                    {
                        _webDriver = new ChromeDriver();
                    }
                    return _webDriver;
                }
            }
        }
    }
}
