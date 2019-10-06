using OpenQA.Selenium;

namespace Selenium.Functions.Browser
{
    public abstract class Browser
    {
        private Browsers _browser;

        public Browser(Browsers browser)
        {
            _browser = browser;
        }

        public abstract DriverOptions ChromeOptions();
    }
}